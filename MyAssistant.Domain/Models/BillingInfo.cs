using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    [Table("BillingInfo")]
    public class BillingInfo : AuditableEntity, IShareable<BillingInfo>
    {
        public Guid ParentEntityId { get; set; }
        
        public string Title { get; set; } 

        public string ParentEntityType { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }
 
        public DateTime? PaidAt { get; set; } 

        public Guid? PayerId { get; set; }

        // IShareable Implementation
        public ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();
        public bool NotifyOwnerOnChange { get; set; }

        public BillingInfo() { }

        public BillingInfo(IBillable<IEntityBase> parent)
        {
            ParentEntityId = parent.Id;
            ParentEntityType = parent.GetType().Name;
        }
    }
}
