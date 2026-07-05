using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnOut.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
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
    public IActionResult Me()
    {
        return Ok(new
        {
            isAuthenticated = User.Identity?.IsAuthenticated ?? false,
            name = User.Identity?.Name,
            claims = User.Claims.Select(c => new
            {
                c.Type,
                c.Value
            })
        });
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
}