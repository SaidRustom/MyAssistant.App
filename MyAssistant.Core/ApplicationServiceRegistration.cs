using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyAssistant.Core.Features.Base.Create;
using MyAssistant.Core.Features.Base.Get;
using MyAssistant.Core.Features.Base.Update;
using MyAssistant.Core.Profiles;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Shared;

namespace MyAssistant.Core
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            
            // FluentValidation.AspNetCore package does auto-registration nicely:
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register closed generic types
            // Find all types implementing IEntityBase (not abstract, not interface)
            var entityAssembly = typeof(IEntityBase).Assembly; // use domain/entities assembly
            var entityTypes = entityAssembly
                .GetTypes()
                .Where(t => typeof(IEntityBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            // Find all IDto<> implementations (concrete classes)
            var dtoAssembly = typeof(IDto<>).Assembly;
            var dtoTypes = dtoAssembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDto<>)))
                .ToList();

            // Register the handler
            foreach (var entityType in entityTypes)
            {
                //Find the DTOs having entityType as a generic argument
                var dtoType = dtoTypes.FirstOrDefault(dto =>
                    dto.GetInterfaces()
                        .Any(i =>
                            i.IsGenericType
                            && i.GetGenericTypeDefinition() == typeof(IDto<>)
                            && i.GetGenericArguments()[0] == entityType
                        )
                );

                if (dtoType != null)
                {
                    var getEntityByIdRequestType = typeof(GetEntityByIdQuery<,>).MakeGenericType(entityType, dtoType);
                    var getEntityByIdHandlerType = typeof(GetEntityByIdQueryHandler<,>).MakeGenericType(entityType, dtoType);
                    var getEntityByIdServiceType = typeof(IRequestHandler<,>).MakeGenericType(getEntityByIdRequestType, dtoType);
                    services.AddScoped(getEntityByIdServiceType, getEntityByIdHandlerType);
                }
                var createEntityRequestType = typeof(CreateEntityCommand<>).MakeGenericType(entityType);
                var createEntityHandlerType = typeof(CreateEntityCommandHandler<>).MakeGenericType(entityType);
                var createEntityServiceType = typeof(IRequestHandler<,>).MakeGenericType(createEntityRequestType, typeof(Guid));
                services.AddScoped(createEntityServiceType, createEntityHandlerType);

                var updateEntityRequestType = typeof(UpdateEntityCommand<>).MakeGenericType(entityType);
                var updateEntityHandlerType = typeof(UpdateEntityCommandHandler<>).MakeGenericType(entityType);
                var updateEntityServiceType = typeof(IRequestHandler<,>).MakeGenericType(updateEntityRequestType, typeof(Guid));
                services.AddScoped(updateEntityServiceType, updateEntityHandlerType);
            }
            return services;
        }
    }
}
