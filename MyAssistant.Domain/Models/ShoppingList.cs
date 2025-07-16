using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Domain.Models
{
    /// <summary>
    /// Represents a shopping list entity, including its items, related billing information,
    /// and sharing capabilities
    /// </summary>
    [Table("ShoppingList")]
    public class ShoppingList : AuditableEntity, IShareable<ShoppingList>, IBillable<ShoppingList>
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required, Range(1, 15)]
        public int ShoppingListTypeCode { get; set; }

        public virtual ShoppingListType ShoppingListType { get; set; } = new();

        public virtual ICollection<ShoppingListItem> Items { get; set; } = new List<ShoppingListItem>();

        // IShareable Implementation
        public virtual ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

        // IBillable Implementation
        public ICollection<BillingInfo>? Bills { get; set; } 
    }
}
