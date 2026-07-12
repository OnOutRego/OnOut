using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using OnOut.Application.Features.Hasher.Commands.UpdateCommand;

namespace OnOut.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("login")]
    public IActionResult Login([FromQuery] string? returnUrl = "/")
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = returnUrl
            },
            OpenIdConnectDefaults.AuthenticationScheme);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var subject = User.FindFirst("sub")?.Value;
        var email = User.FindFirst("email")?.Value;
        var name = User.FindFirst("name")?.Value;

        if (string.IsNullOrWhiteSpace(subject))
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new EnsureHasherFromAuth0Command
        {
            Subject = subject,
            Email = email,
            Name = name
        });

        return Ok(result);
    }

    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme);
    }

    [Authorize]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateHasherCommand command)
    {
        if (command == null)
        {
            return BadRequest();
        }

        var updated = await _mediator.Send(command);
        return Ok(updated);
    }
}