using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    /// <summary>
    /// Represents a task item with scheduling, completion, priority, recurrence, and sharing capabilities.
    /// </summary>
    [Table("TaskItem")]
    public class TaskItem : AuditableEntity, IShareable<TaskItem>, IRecurrable
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? ScheduledAt { get; set; }

        public DateTime? DueDate { get; set; }

        public int LengthInMinutes { get; set; }

        public bool IsCompleted { get; set; }

        [Range(0,5)]
        public int Priority { get; set; }

        // IRecurrable implementation:
        public Guid? RecurrenceId { get; set; }
        public virtual Recurrence? Recurrence { get; set; }

        // IShareable Implementation
        public virtual ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();
        public bool NotifyOwnerOnChange { get; set; }

        // Relationships
        public Guid? GoalId { get; set; }
        public virtual Goal? LinkedGoal { get; set; }
    }
}
