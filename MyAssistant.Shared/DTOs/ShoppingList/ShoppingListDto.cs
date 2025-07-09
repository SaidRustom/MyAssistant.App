using MyAssistant.Domain.Base;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class ShoppingListDto : CreateOrUpdateShoppingListCommand, IDto<ShoppingList>
{
    public new Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public ICollection<ShoppingListItemDto> Items { get; set; } = new List<ShoppingListItemDto>();
    
    //TODO: Look into creating dto for shares..
    public ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

    //TODO: add this once billingDTO is implemented
    //public ICollection<BillingInfo>? Bills { get; set; } 
}