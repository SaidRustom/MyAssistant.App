using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyAssistant.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyAssistantDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyAssistantDbConnectionString")));

            return services;
        }
    }
}