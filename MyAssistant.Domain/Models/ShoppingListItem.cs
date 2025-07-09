using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    [Table("ShoppingListItem")]
    public class ShoppingListItem : EntityBase, IRecurrable
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = default!;

        public int Quantity { get; set; }

        public bool IsActive { get; set; } = true;

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
