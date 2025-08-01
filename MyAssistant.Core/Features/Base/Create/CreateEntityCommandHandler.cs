using System.Reflection;
using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;
using FluentValidation;

namespace MyAssistant.Core.Features.Base.Create
{

    public interface ICreateEntityCommandHandler<TEntity>
        : IRequestHandler<CreateEntityCommand<TEntity>, Guid>
        where TEntity : class, IEntityBase
    { }

    /// <summary>
    /// Handles the creation of a new entity by validating the provided entity,
    /// persisting it to the repository, and returning its ID.
    /// Throws <see cref="ArgumentNullException"/> if the request or entity is null.
    /// </summary>
    public class CreateEntityCommandHandler<TEntity>(
        IBaseAsyncRepository<TEntity> repository,
        IServiceProvider serviceProvider)
        : ICreateEntityCommandHandler<TEntity>
        where TEntity : class, IEntityBase, new()
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task<Guid> Handle(CreateEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (request.Entity is null) throw new ArgumentNullException(nameof(request.Entity));

            await ValidateEntityAsync(request.Entity, cancellationToken);

            //request.Entity.Id = Guid.NewGuid(); Handled by SaveChangesAsync in DbContext 

            await repository.AddAsync(request.Entity);

            return request.Entity.Id;
        }

        /// <summary>
        /// Validates the provided entity using a runtime-discovered validator implementing <see cref="AbstractValidator{TEntity}"/>.
        /// Searches the current assembly for a non-abstract, non-interface validator type, 
        /// resolves it from the service provider, and performs asynchronous validation.
        /// Throws <see cref="ValidationException"/> if the entity fails validation. 
        /// </summary>
        private async Task ValidateEntityAsync(TEntity entity, CancellationToken cancellationToken)
        {
            //Runtime Validator Auto-discovery, could be improved....

            var validatorType = typeof(AbstractValidator<TEntity>);
            var assembly = Assembly.GetExecutingAssembly();

            var foundValidatorType = assembly.GetTypes()
                .FirstOrDefault(t =>
                    !t.IsAbstract &&
                    !t.IsInterface &&
                    validatorType.IsAssignableFrom(t)
                );

            if (foundValidatorType != null)
            {
                var validator = (IValidator<TEntity>?)_serviceProvider.GetService(foundValidatorType);

                if (validator is not null)
                {
                    var result = await validator.ValidateAsync(entity, cancellationToken);
                    if (!result.IsValid)
                        throw new Exceptions.ValidationException(result);
                }
            }
        }
    }
}