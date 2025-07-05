using MediatR;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Features.Base.Update
{
    public class UpdateEntityCommand<TEntity>(TEntity entity) : IRequest<Guid>
        where TEntity : class, IEntityBase
    {
        public TEntity Entity { get; } = entity;
    }
}
