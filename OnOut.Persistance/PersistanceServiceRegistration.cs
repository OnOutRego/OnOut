using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnOut.Persistance.DatabaseContext;

namespace OnOut.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration config) 
        {
            services.AddDbContext<OnOutDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("OnOutConnectionString"));
            });
            
            
            return services;
        }
    }
}
