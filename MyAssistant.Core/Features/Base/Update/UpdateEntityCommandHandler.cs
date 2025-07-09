using FluentValidation;
using System.Reflection;
using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Features.Base.Update
{
    public interface IUpdateEntityCommandHandler<TEntity>
    : IRequestHandler<UpdateEntityCommand<TEntity>, Guid>
    where TEntity : class, IEntityBase
    { }

    /// <summary>
    /// Handles the update operation for a given entity.
    /// Trigger the Validator of the provided entity, and updates it in the repository.
    /// Returns the unique identifier of the updated entity.
    /// Throws <see cref="ArgumentNullException"/> if the request or entity is null.
    /// </summary>
    public class UpdateEntityCommandHandler<TEntity>(
    IBaseAsyncRepository<TEntity> repository,
    IServiceProvider serviceProvider)
    : IUpdateEntityCommandHandler<TEntity>
    where TEntity : class, IEntityBase, new()
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task<Guid> Handle(UpdateEntityCommand<TEntity> request, CancellationToken cancellationToken)
        {
            if (request is null) throw new ArgumentNullException(nameof(request));
            if (request.Entity is null) throw new ArgumentNullException(nameof(request.Entity));

            await ValidateEntityAsync(request.Entity, cancellationToken);

            await repository.UpdateAsync(request.Entity);

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
                        throw new ValidationException(result.Errors);
                }
            }
        }
    }
}
