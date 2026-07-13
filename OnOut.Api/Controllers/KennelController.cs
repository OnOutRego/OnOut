using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnOut.Application.Features.Kennel;
using OnOut.Application.Features.Kennel.Commands.CreateKennel;
using OnOut.Application.Features.Kennel.Commands.DeleteKennel;
using OnOut.Application.Features.Kennel.Commands.UpdateKennel;
using OnOut.Application.Features.Kennel.Queries.GetAll;
using OnOut.Application.Features.Kennel.Queries.GetWithDetails;

namespace OnOut.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/kennels")]
public class KennelController : ControllerBase
{
    private readonly IMediator _mediator;

    public KennelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<KennelListDto>>> GetAll(CancellationToken cancellationToken)
    {
        var kennels = await _mediator.Send(new GetAllKennelsQuery(), cancellationToken);
        return Ok(kennels);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<KennelDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var kennel = await _mediator.Send(new GetKennelWithDetailsQuery { Id = id }, cancellationToken);
        return Ok(kennel);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateKennelCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var createdId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = createdId }, createdId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateKennelCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        if (id != command.Id)
        {
            return BadRequest("Route id must match kennel id in request body.");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteKennelCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}
