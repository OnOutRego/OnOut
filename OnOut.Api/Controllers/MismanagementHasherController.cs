using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnOut.Application.Features.MismanagmentHasher.Commands.CreateMismanagmentHasher;
using OnOut.Application.Features.MismanagmentHasher.Commands.DeleteMismanagmentHasher;
using OnOut.Application.Features.MismanagmentHasher.Commands.UpdateMismanagementHasher;
using OnOut.Application.Features.MismanagmentHasher.Queries;
using OnOut.Application.Features.MismanagmentHasher.Queries.GetByHasher;
using OnOut.Application.Features.MismanagmentHasher.Queries.GetByKennel;

namespace OnOut.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/mismanagement-hashers")]
public class MismanagementHasherController : ControllerBase
{
    private readonly IMediator _mediator;

    public MismanagementHasherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("hasher/{hasherId:guid}")]
    public async Task<ActionResult<List<MismanagementHasherDto>>> GetByHasher(Guid hasherId, CancellationToken cancellationToken)
    {
        var hashers = await _mediator.Send(new GetByHasherQuery { HasherId = hasherId }, cancellationToken);
        return Ok(hashers);
    }

    [HttpGet("kennel/{kennelId:guid}")]
    public async Task<ActionResult<List<MismanagementHasherDto>>> GetByKennel(Guid kennelId, CancellationToken cancellationToken)
    {
        var hashers = await _mediator.Send(new GetByKennelQuery { KennelId = kennelId }, cancellationToken);
        return Ok(hashers);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateMismanagmentHasherCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var createdId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(null, null, createdId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMismanagementHasherCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        if (id != command.MismanagmentHasherId)
        {
            return BadRequest("Route id must match mismanagement hasher id in request body.");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteMismanagementHasherCommand { Id = id }, cancellationToken);
        return NoContent();
    }
}
