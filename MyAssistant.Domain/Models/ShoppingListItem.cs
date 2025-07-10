using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    /// <summary>
    /// Represents an item within a shopping list, including details such as quantity, pricing, recurrence, and related shopping list.
    /// </summary>
    [Table("ShoppingListItem")]
    public class ShoppingListItem : AuditableEntity, IRecurrable
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = default!;

        public int Quantity { get; set; }

        public bool IsActive { get; set; } = true;

        public int TotalPurchaseCount { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? TotalPrice { get; set; }

        public DateTime? LastPurchaseDate { get; set; }

        // IRecurrable implementation:
        public Guid? RecurrenceId { get; set; }
        public virtual Recurrence? Recurrence { get; set; }

        // Relationships
        [Required]
        public Guid ShoppingListId { get; set; }
        public virtual ShoppingList ShoppingList { get; set; } = default!;
    }
}
