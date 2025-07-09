using FluentValidation;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Validators;

public class ShoppingListItemValidator : AbstractValidator<ShoppingListItem>
{
    private readonly IBaseAsyncRepository<ShoppingListItem> _itemRepo;
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IBaseAsyncRepository<ShoppingList> _shoppingListRepo ;

    public ShoppingListItemValidator(
        IBaseAsyncRepository<ShoppingListItem> itemRepo,
        IBaseAsyncRepository<ShoppingList> shoppingListRepo,
        ILoggedInUserService loggedinUserService)
    {
        _itemRepo = itemRepo;
        _shoppingListRepo = shoppingListRepo;
        _loggedInUserService = loggedinUserService;
        
        RuleFor(x => x)
            .MustAsync(ListExistsAsync).WithMessage("Shopping list specified for this item doesn't exist");
        
        RuleFor(x => x)
            .MustAsync(ItemAlreadyExists).WithMessage("An item with the same name already exists");

        RuleFor(p => p)
            .MustAsync(UserCanEditAsync).WithMessage("You do not have permission to edit this item");
    }
    
    async Task<bool> ListExistsAsync(ShoppingListItem item, CancellationToken token)
    {
        var list = await _shoppingListRepo.GetByIdAsync(item.ShoppingListId);
        return list != null;
    }
    
    async Task<bool> ItemAlreadyExists(ShoppingListItem item, CancellationToken token)
    {
        var list = await _shoppingListRepo.GetByIdAsync(item.ShoppingListId);
            //result has to be false if validation fails.. flip
        return !(list != null && list.Items.Any(x => x.Name == item.Name));
    }

    /// <summary>
    /// ShoppingListItem doesn't implement Shareable, need to handle user permission here
    /// </summary>
    async Task<bool> UserCanEditAsync(ShoppingListItem item, CancellationToken token)
    {
        var list = await _shoppingListRepo.GetByIdAsync(item.ShoppingListId);
        
        if(list == null)
            return false;
        
        bool isListOwner = list.UserId.Equals(_loggedInUserService.UserId);
        bool canEditList = list.Shares?
            .FirstOrDefault(x => 
                x.UserId.Equals(_loggedInUserService.UserId))?.PermissionTypeCode != PermissionType.Read;
        
        return isListOwner || canEditList;
    }
}