
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnOut.Application.Features.Event;
using OnOut.Application.Features.Event.Commands.CreateEvent;
using OnOut.Application.Features.Event.Commands.DeleteEvent;
using OnOut.Application.Features.Event.Commands.UpdateEvent;
using OnOut.Application.Features.Event.Queries.GetAll;
using OnOut.Application.Features.Event.Queries.GetWithDetails;
using OnOut.Application.Contracts;

namespace OnOut.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHasherRepository _hasherRepository;

    public EventController(IMediator mediator, IHasherRepository hasherRepository)
    {
        _mediator = mediator;
        _hasherRepository = hasherRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<EventListDto>>> GetAll(CancellationToken cancellationToken)
    {
        var events = await _mediator.Send(new GetAllEventQuery(), cancellationToken);
        return Ok(events);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EventDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var @event = await _mediator.Send(new GetOneWithDetailsQuery { EventId = id }, cancellationToken);
        return Ok(@event);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var subject = User.FindFirst("sub")?.Value;
        if (string.IsNullOrWhiteSpace(subject))
        {
            return Unauthorized();
        }

        var creator = await _hasherRepository.GetByAuth0SubjectAsync(subject);
        if (creator == null)
        {
            return Unauthorized();
        }

        command.CreatorId = creator.Id;

        var createdId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = createdId }, createdId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEventCommand command, CancellationToken cancellationToken)
    {
        if (command == null || command.Event == null)
        {
            return BadRequest();
        }

        if (id != command.Event.Id)
        {
            return BadRequest("Route id must match event id in request body.");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteEventCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}