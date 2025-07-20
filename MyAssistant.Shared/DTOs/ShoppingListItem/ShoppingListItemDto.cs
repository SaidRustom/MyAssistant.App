using MyAssistant.Domain.Base;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class ShoppingListItemDto : CreateOrUpdateShoppingListItem, IDto<ShoppingListItem>
{
    public new Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public DateTime? LastPurchaseDate { get; set; }

    public int TotalPurchaseCount { get; set; }

    public bool IsRecurring { get; set; }
    public LookupDto RecurrenceType { get; set; } 
    public DateTime? NextOccurrenceDate { get; set; }

    public ICollection<AuditLog> AuditLogs { get; set; }
}