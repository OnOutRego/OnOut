using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnOut.Application.Contracts;
using OnOut.Persistance.DatabaseContext;
using OnOut.Persistance.Repositories;

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
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IHasherRepository, HasherRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<IKennelRepository, KennelRepository>();
            services.AddScoped<IRegoRepository, RegoRepository>();
            
            return services;
        }
    }
}
