using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Features.Base.GetList;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Shared;

public class GetEntityListQueryHandler<TEntity, TResponse> : IRequestHandler<GetEntityListQuery<TEntity, TResponse>, PaginatedList<TResponse>>
    where TEntity : class, IEntityBase
    where TResponse : IDto<TEntity>
{
    private readonly IBaseAsyncRepository<TEntity> _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedInUserService _loggedInUserService;

    public GetEntityListQueryHandler(IBaseAsyncRepository<TEntity> repository, IMapper mapper, ILoggedInUserService loggedInUserService)
    {
        _repository = repository;
        _mapper = mapper;
        _loggedInUserService = loggedInUserService;
    }

    public async Task<PaginatedList<TResponse>> Handle(GetEntityListQuery<TEntity, TResponse> request, CancellationToken cancellationToken)
    {
        // Retrieve all, then sort and page in the repository
        var (entities, totalCount) = await _repository.GetPagedListAsync(
            _loggedInUserService.UserId,
            filter: null,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);

        var dtos = _mapper.Map<List<TResponse>>(entities);

        var shareableDtoInterface = dtos.First()
               .GetType()
               .GetInterfaces()
               .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IShareableDto<>));

        if(shareableDtoInterface != null)

        foreach (var dto in dtos)
        {
                var permission = await _repository.GetUserPermissionTypeAsync(dto.Id, _loggedInUserService.UserId);

                //Populate PermissionType in the returned Dto (Interface enforced)
                var permissionProp = dto.GetType().GetProperty("PermissionType");
                permissionProp!.SetValue(dto, permission, null);
            }

        return new PaginatedList<TResponse>(dtos, totalCount, request.PageNumber, request.PageSize);
    }
}