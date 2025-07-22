using MyAssistant.Domain.Base;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class ShoppingListDto : CreateOrUpdateShoppingListCommand, IDto<ShoppingList>, IShareableDto<ShoppingList>
{
    public new Guid Id { get; set; }
    
    public Guid UserId { get; set; }

    public DateOnly CreatedDate { get; set; }

    public bool NotifyOwnerOnChange { get; set; }

    public LookupDto ShoppingListType { get; set; }

    public ICollection<ShoppingListItemDto> Items { get; set; } = new List<ShoppingListItemDto>();
    
    //TODO: Look into creating dto for shares..
    public ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

    public LookupDto PermissionType { get; set; } 

    //TODO: add this once billingDTO is implemented
    //public ICollection<BillingInfo>? Bills { get; set; } 
}