using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.ShoppingLists;

public class GetShoppingListQueryHandler : IRequestHandler<GetShoppingListQuery, ShoppingListDto>
{
    private readonly IShoppingListRepository _repo;
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IMapper _mapper;

    public GetShoppingListQueryHandler(
        IShoppingListRepository repo, 
        ILoggedInUserService loggedInUserService,
        IMapper mapper)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _loggedInUserService = loggedInUserService ?? throw new ArgumentNullException(nameof(loggedInUserService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ShoppingListDto> Handle(GetShoppingListQuery command, CancellationToken cancellationToken)
    {
        await _repo.ValidateCanGetAsync(command.Id, _loggedInUserService.UserId);

        var item = await _repo.GetByIdAsync(command.Id);

        var dto = _mapper.Map<ShoppingListDto>(item);

        dto.PermissionType = await _repo.GetUserPermissionTypeAsync(command.Id, _loggedInUserService.UserId);
        

        return dto;
    }
}
