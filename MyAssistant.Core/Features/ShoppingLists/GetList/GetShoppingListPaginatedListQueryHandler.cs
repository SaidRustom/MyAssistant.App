using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Responses;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.ShoppingLists.GetList
{
    public class GetShoppingListPaginatedListQueryHandler : IRequestHandler<GetShoppingListPaginatedListQuery, PaginatedList<ShoppingListDto>>
    {
        private readonly IShoppingListRepository _repo;
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly IMapper _mapper;

        public GetShoppingListPaginatedListQueryHandler(IShoppingListRepository repo,
        ILoggedInUserService loggedInUserService,
        IMapper mapper)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginatedList<ShoppingListDto>> Handle(GetShoppingListPaginatedListQuery cmd, CancellationToken cancellationToken)
        {
            // Retrieve all, then sort and page in the repository
            var (entities, totalCount) = await _repo.GetPagedListAsync(
                _loggedInUserService.UserId,
                filter: null,
                pageNumber: cmd.PageNumber,
                pageSize: cmd.PageSize);

            var dtos = _mapper.Map<List<ShoppingListDto>>(entities);

            foreach(var dto in dtos)
            {
                dto.PermissionType = _mapper.Map<LookupDto>(await _repo.GetUserPermissionTypeAsync(dto.Id, _loggedInUserService.UserId));
            }

            return new PaginatedList<ShoppingListDto>(dtos, totalCount, cmd.PageNumber, cmd.PageSize);
        }
    }
}
