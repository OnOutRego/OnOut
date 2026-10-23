using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnOut.Application;
using OnOut.Infrastructure;
using OnOut.Persistance;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var auth0Authority = builder.Configuration["Auth0:Authority"];
var auth0Audience = builder.Configuration["Auth0:Audience"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        if (!string.IsNullOrWhiteSpace(auth0Authority))
        {
            options.Authority = auth0Authority.TrimEnd('/');
        }
        else
        {
            var auth0Domain = builder.Configuration["Auth0:Domain"];
            if (!string.IsNullOrWhiteSpace(auth0Domain))
            {
                options.Authority = $"https://{auth0Domain.TrimEnd('/')}/";
            }
        }

        if (!string.IsNullOrWhiteSpace(auth0Audience))
        {
            options.Audience = auth0Audience;
        }
        else
        {
            options.TokenValidationParameters.ValidateAudience = false;
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "roles",
            ValidateAudience = !string.IsNullOrWhiteSpace(auth0Audience)
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("GeneralAdmin", policy =>
        policy.RequireRole("GeneralAdmin"));
});

builder.Services.AddControllers();

// Built-in ASP.NET Core OpenAPI generation
builder.Services.AddOpenApi();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // OpenAPI document:
    // https://localhost:<port>/openapi/v1.json
    app.MapOpenApi();

    // Scalar interface:
    // https://localhost:<port>/scalar/v1
    app.MapScalarApiReference();
}
app.UseRouting();
// Only enable HTTPS redirection in non-development environments to avoid
// "Failed to determine https port for redirect" when running the app
// with HTTP-only endpoints during local development.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowReact");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();