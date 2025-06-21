using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    public class BillingInfo : AuditableEntity, IShareable
    {
        public Guid ParentEntityId { get; set; }

        public string ParentEntityType { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }
 
        public DateTime? PaidAt { get; set; } 

        public Guid? Payer { get; set; }

        // IShareable Implementation
        public virtual ICollection<EntityShare> Shares { get; set; } = new List<EntityShare>();
    }
}
