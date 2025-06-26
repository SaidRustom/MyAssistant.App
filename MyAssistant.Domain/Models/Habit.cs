using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Domain.Models
{
    public class Habit : AuditableEntity, IShareable<Habit>, IBillable<Habit>, IRecurrable
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int GoalValue { get; set; } // E.g: # days, # times, etc.

        public int Progress { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsCompleted { get; set; }

        // IRecurrable implementation:
        public bool IsRecurring { get; set; }
        public int? RecurrenceTypeCode { get; set; }
        public DateTime? RecurrenceEndDate { get; set; } // In this case the end date basically..

        // IShareable Implementation
        public virtual ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

        // IBillable Implementation
        public virtual ICollection<BillingInfo>? Bills { get; set; } = new List<BillingInfo>();

        //Relationships
        public Guid? GoalId { get; set; }
        public virtual Goal? LinkedGoal { get; set; }
    }
}
