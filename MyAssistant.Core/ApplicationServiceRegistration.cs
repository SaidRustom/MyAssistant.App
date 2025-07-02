using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyAssistant.Core.Features.Base.Create;
using MyAssistant.Core.Features.Base.Get;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            
            // Register closed generic types, one for each entity type..
            // 1. Find all types implementing IEntityBase (not abstract, not interface)
            var entityAssembly = typeof(IEntityBase).Assembly; // use your domain/entities assembly here
            var entityTypes = entityAssembly
                .GetTypes()
                .Where(t => typeof(IEntityBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            // 2. Register the handler for each entity type
            foreach (var entityType in entityTypes)
            {
                var getEntityByIdRequestType = typeof(GetEntityByIdQuery<>).MakeGenericType(entityType);
                var getEntityByIdHandlerType = typeof(GetEntityByIdQueryHandler<>).MakeGenericType(entityType);
                var getEntityByIdServiceType = typeof(IRequestHandler<,>).MakeGenericType(getEntityByIdRequestType, entityType);
                services.AddScoped(getEntityByIdServiceType, getEntityByIdHandlerType);
                
                var createEntityRequestType = typeof(CreateEntityCommand<>).MakeGenericType(entityType);
                var createEntityHandlerType = typeof(CreateEntityCommandHandler<>).MakeGenericType(entityType);
                var createEntityServiceType = typeof(IRequestHandler<,>).MakeGenericType(createEntityRequestType, typeof(Guid));
                services.AddScoped(createEntityServiceType, createEntityHandlerType);
            }
            return services;
        }
    }
}
