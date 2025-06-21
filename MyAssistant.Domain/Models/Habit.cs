using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    public class Habit : AuditableEntity, IShareable
    {
        [Required]
        public Guid UserId { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public int GoalValue { get; set; } // E.g: # days, # times, etc.

        public int Progress { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCompleted { get; set; }

        // IRecurrable implementation:
        public bool IsRecurring { get; set; }
        public IRecurrable.RecurrenceType? RecurrencePattern { get; set; }

        // IShareable Implementation
        public virtual ICollection<EntityShare> Shares { get; set; } = new List<EntityShare>();

        //Relationships
        public Guid? GoalId { get; set; }
        public virtual Goal? LinkedGoal { get; set; }
    }
}
