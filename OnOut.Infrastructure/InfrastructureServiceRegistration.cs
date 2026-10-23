using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnOut.Application.Contracts.Logging;
using OnOut.Infrastructure.Logging;

namespace OnOut.Infrastructure;

    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}

