using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnOut.Application;
using OnOut.Infrastructure;
using OnOut.Persistance;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var auth0Domain = builder.Configuration["Auth0:Domain"];
var auth0Audience = builder.Configuration["Auth0:Audience"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{auth0Domain}/";
        options.Audience = auth0Audience;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "roles"
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

app.UseHttpsRedirection();
app.UseCors("AllowReact");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();