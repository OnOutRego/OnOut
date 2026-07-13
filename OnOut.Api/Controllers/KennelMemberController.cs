using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnOut.Application.Features.KennelMember.Commands.CreateKennelMember;
using OnOut.Application.Features.KennelMember.Commands.DeleteKennelMember;
using OnOut.Application.Features.KennelMember.Commands.UpdateKennelMember;

namespace OnOut.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/kennel-members")]
public class KennelMemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public KennelMemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateKennelMemberCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var createdId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(null, null, createdId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateKennelMemberCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        if (id != command.KennelMemberId)
        {
            return BadRequest("Route id must match kennel member id in request body.");
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteKennelMemberCommand { KennelMemberId = id }, cancellationToken);
        return NoContent();
    }
}
