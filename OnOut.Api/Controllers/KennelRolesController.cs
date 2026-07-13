using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnOut.Application.Features.KennelRoles.Commands.CreateKennelRole;
using OnOut.Application.Features.KennelRoles.Commands.DeleteKennelRole;

namespace OnOut.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/kennel-roles")]
public class KennelRolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public KennelRolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateKennelRoleCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var createdId = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(null, null, createdId);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteKennelRoleCommand { RoleId = id }, cancellationToken);
        return NoContent();
    }
}
