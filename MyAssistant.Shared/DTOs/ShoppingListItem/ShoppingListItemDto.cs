using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class ShoppingListItemDto : IDto<ShoppingListItem>, IMapWith<ShoppingListItem>
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = default!;

    public int Quantity { get; set; }

    public bool IsActive { get; set; } = true;

    public int TotalPurchaseCount { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? LastPurchaseDate { get; set; }

    public bool IsRecurring { get; set; }
    public LookupDto RecurrenceType { get; set; }
    public int RecurrenceInterval { get; set; } = 1;
    public DateTime? NextOccurrenceDate { get; set; }

    public Guid ShoppingListId { get; set; }

    public ICollection<AuditLog> AuditLogs { get; set; }
}