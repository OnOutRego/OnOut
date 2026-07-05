using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using OnOut.Application.Contracts.Logging;
using OnOut.Infrastructure.Logging;

namespace OnOut.Infrastructure;

    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.Authority = configuration["Auth0:Authority"];
                options.ClientId = configuration["Auth0:ClientId"];
                options.ClientSecret = configuration["Auth0:ClientSecret"];

                options.ResponseType = "code";
                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");
                options.RequireHttpsMetadata = true;

                options.CallbackPath = "/signin-oidc";
            });

        services.AddAuthorization();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}

