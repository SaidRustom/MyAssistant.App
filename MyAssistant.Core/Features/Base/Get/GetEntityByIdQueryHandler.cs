using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Features.Base.Get;

public interface IGetEntityByIdQueryHandler<TEntity> : IRequestHandler<GetEntityByIdQuery<TEntity>, TEntity>
    where TEntity : class, IEntityBase
{ }

public class GetEntityByIdQueryHandler<TEntity>(IBaseAsyncRepository<TEntity> repository)
    : IGetEntityByIdQueryHandler<TEntity>
    where TEntity : class, IEntityBase
{
    public async Task<TEntity> Handle(GetEntityByIdQuery<TEntity> request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var entity = await repository.GetByIdAsync(request.Id);
        if (entity == null)
            throw new KeyNotFoundException($"{typeof(TEntity).Name} with id '{request.Id}' was not found.");

        return entity;
    }
}