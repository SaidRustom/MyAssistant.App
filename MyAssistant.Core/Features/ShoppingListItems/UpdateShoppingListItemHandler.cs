using AutoMapper;
using FluentValidation;
using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Validators;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.ShoppingListItems;

public class UpdateShoppingListItemHandler : IRequestHandler<CreateOrUpdateShoppingListItem, Guid>
{
    private readonly IBaseAsyncRepository<ShoppingListItem> _repo;
    private readonly IBaseAsyncRepository<ShoppingList> _listRepo;
    private ILoggedInUserService  _loggedInUserService;
    private readonly IMapper _mapper;

    public UpdateShoppingListItemHandler(
        IBaseAsyncRepository<ShoppingListItem> repo,
        IBaseAsyncRepository<ShoppingList> listRepo,
        ILoggedInUserService loggedInUserService,
        IMapper mapper)
    {
        _repo = repo;
        _listRepo = listRepo;
        _mapper = mapper;
        _loggedInUserService = loggedInUserService;
    }

    public async Task<Guid> Handle(CreateOrUpdateShoppingListItem cmd, CancellationToken cancellationToken)
    {
        var obj = _mapper.Map<ShoppingListItem>(cmd);
        
        var validator = new ShoppingListItemValidator(_repo, _listRepo, _loggedInUserService);
        await validator.ValidateAndThrowAsync(obj, cancellationToken);
        
        var dbValue = (await _repo.GetByIdAsync(cmd.Id.Value));
        await _repo.DetachAsync(dbValue); //we don't want EF to track this..

        if (dbValue != null && dbValue.IsActive != cmd.IsActive && !cmd.IsActive)
        {
            obj.TotalPurchaseCount = obj.TotalPurchaseCount + obj.Quantity;
            obj.Quantity = 0;
            obj.LastPurchaseDate = DateTime.Now;
        }

        await _repo.UpdateAsync(obj);
        return cmd.Id.Value;
    }
}