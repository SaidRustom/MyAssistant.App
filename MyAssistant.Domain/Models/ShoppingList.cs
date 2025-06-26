using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    public class ShoppingList : AuditableEntity, IShareable<ShoppingList>, IBillable<ShoppingList>
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        public virtual ICollection<ShoppingListItem> Items { get; set; } = new List<ShoppingListItem>();

        // IShareable Implementation
        public virtual ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

        // IBillable Implementation
        public ICollection<BillingInfo>? Bills { get; set; } 
    }
}
