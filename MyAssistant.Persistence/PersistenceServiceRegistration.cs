﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Persistence.Repositories.Base;
using MyAssistant.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Design;
using MyAssistant.Core.Contracts.Persistence.Service;
using MyAssistant.Persistence.Repositories.Service;

namespace MyAssistant.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyAssistantDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MyAssistantIdentityDbConnectionString")));

            services.AddScoped(typeof(IBaseAsyncRepository<>), typeof(BaseAsyncRepository<>));
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<IRecurrenceRepository, RecurrenceRepository>();

            services.AddScoped<IRecurringShoppingListItemActivationRepository, RecurringShoppingListItemActivationRepository>();


            return services;
        }

        public class MyAssistantDbContextFactory : IDesignTimeDbContextFactory<MyAssistantDbContext>
        {
            public MyAssistantDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<MyAssistantDbContext>();

                optionsBuilder.UseSqlServer("Server=tcp:sqlserver510.database.windows.net,1433;Initial Catalog=MyAssistantIdentity;Persist Security Info=False;User ID=said_rustom;Password=Man_man123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");

                return new MyAssistantDbContext(optionsBuilder.Options);
            }
        }
    }
}