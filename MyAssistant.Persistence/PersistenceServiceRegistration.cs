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
            services.AddDbContextFactory<MyAssistantDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("Data Source=data.db")));

            services.AddScoped(typeof(IBaseAsyncRepository<>), typeof(BaseAsyncRepository<>));
            services.AddScoped<IGoalRepository, GoalRepository>();

            return services;
        }

        public class MyAssistantDbContextFactory : IDesignTimeDbContextFactory<MyAssistantDbContext>
        {
            public MyAssistantDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MyAssistantDbContext>();

                optionsBuilder.UseSqlite("Data Source=data.db");

                return new MyAssistantDbContext(optionsBuilder.Options);
            }
        }
    }
}