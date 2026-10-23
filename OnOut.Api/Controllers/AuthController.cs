using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OnOut.Application.Features.Hasher.Commands.UpdateCommand;

namespace OnOut.Api.Controllers;

[ApiController]
[Route("api/auth")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var subject = User.FindFirst("sub")?.Value;
        if (string.IsNullOrWhiteSpace(subject))
        {
            return Unauthorized();
        }

        var email = User.FindFirst("email")?.Value;
        var name = User.FindFirst("name")?.Value;

        var result = await _mediator.Send(new EnsureHasherFromAuth0Command
        {
            Subject = subject,
            Email = email,
            Name = name
        });

        return Ok(result);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateHasherCommand command)
    {
        var subject = User.FindFirst("sub")?.Value;
        if (command == null || string.IsNullOrWhiteSpace(subject))
        {
            return Unauthorized();
        }

        command.SenderId = subject;
        var updated = await _mediator.Send(command);
        return Ok(updated);
    }
}