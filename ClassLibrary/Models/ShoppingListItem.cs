using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    public class ShoppingListItem : EntityBase, IRecurrable
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = default!;

        public int Quantity { get; set; } = 1;

        public bool IsActive { get; set; } = true;

        public decimal? UnitPrice { get; set; }

        public decimal? TotalPrice { get; set; }

        public DateTime? LastPurchaseDate { get; set; }

        // IRecurrable implementation:
        public bool IsRecurring { get; set; }
        public IRecurrable.RecurrenceType? RecurrencePattern { get; set; }

        // Relationships
        [Required]
        public Guid ShoppingListId { get; set; }
        public virtual ShoppingList ShoppingList { get; set; } = default!;
    }
}
