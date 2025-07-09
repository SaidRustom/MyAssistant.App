using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class ShoppingListItemDto : CreateOrUpdateShoppingListItem, IDto<ShoppingListItem>
{
    public new Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime? LastPurchaseDate { get; set; }

    public virtual Recurrence? Recurrence { get; set; }

    public PermissionType? PermissionType { get; set; }
}