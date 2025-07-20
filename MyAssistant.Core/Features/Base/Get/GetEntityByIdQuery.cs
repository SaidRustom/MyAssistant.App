using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Shared;

namespace MyAssistant.Core.Features.Base.Get;

public class GetEntityByIdQuery<TEntity, TResponse>(Guid id) : IRequest<TResponse>
    where TEntity : class, IEntityBase
    where TResponse : IDto<TEntity>
{
    public Guid Id { get; } = id;
}