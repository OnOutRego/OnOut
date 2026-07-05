using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OnOut.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.AddAutoMapper(assembly);
            return services;

        }
    }
}
