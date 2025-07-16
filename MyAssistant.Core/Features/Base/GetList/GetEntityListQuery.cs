using MediatR;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Shared;

namespace MyAssistant.Core.Features.Base.GetList;

public class GetEntityListQuery<TEntity, TResponse> : IRequest<PaginatedList<TResponse>>
    where TEntity : class, IEntityBase
    where TResponse : IDto<TEntity>
{
    public int PageNumber { get; }
    public int PageSize { get; }

    public GetEntityListQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}