using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnOut.Application.Features.Hasher.Commands.CreateCommand;
using OnOut.Application.Features.Hasher.Commands.DeleteCommand;
using OnOut.Application.Features.Hasher.Commands.UpdateCommand;
using OnOut.Application.Features.Hasher.Queries.GetAll;
using OnOut.Application.Features.Hasher.Queries.GetWithDetails;

namespace OnOut.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/hashers")]
public class HasherController : ControllerBase
{
    private readonly IMediator _mediator;

    public HasherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<HasherListDto>>> GetAll(CancellationToken cancellationToken)
    {
        var hashers = await _mediator.Send(new GetAllHasherQuery(), cancellationToken);
        return Ok(hashers);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HasherDetailsDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var hasher = await _mediator.Send(new GetHasherDetailsQuery { Id = id }, cancellationToken);
        return Ok(hasher);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateHasherCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var createdId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = createdId }, createdId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateHasherCommand command, CancellationToken cancellationToken)
    {
        if (command == null || command.Hasher == null)
        {
            return BadRequest();
        }

        if (id != command.Hasher.Id)
        {
            return BadRequest("Route id must match hasher id in request body.");
        }

        command.SenderId = User.FindFirst("sub")?.Value ?? string.Empty;
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteHasherCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}