using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Persistence.Repositories.Base;
using MyAssistant.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Design;

namespace MyAssistant.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyAssistantDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyAssistantDbConnectionString")));

            services.AddScoped(typeof(IBaseAsyncRepository<>), typeof(BaseAsyncRepository<>));
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();


            return services;
        }

        public class MyAssistantDbContextFactory : IDesignTimeDbContextFactory<MyAssistantDbContext>
        {
            public MyAssistantDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MyAssistantDbContext>();

                optionsBuilder.UseSqlServer("Server=tcp:sqlserver510.database.windows.net,1433;Initial Catalog=MyAssistant;Persist Security Info=False;User ID=said_rustom;Password=Man_man123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");

                return new MyAssistantDbContext(optionsBuilder.Options);
            }
        }
    }
}