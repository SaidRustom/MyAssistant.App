using MediatR;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Features.Base.Get;

public class GetEntityByIdQuery<TEntity>(Guid id) : IRequest<TEntity>
    where TEntity : class, IEntityBase
{
    public Guid Id { get; } = id;
}