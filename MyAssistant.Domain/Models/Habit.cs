﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Domain.Models
{
    [Table("Habit")]
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
        public Guid? RecurrenceId { get; set; }
        public virtual Recurrence? Recurrence { get; set; }

        // IShareable Implementation
        public ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();
        public bool NotifyOwnerOnChange { get; set; }

        // IBillable Implementation
        public ICollection<BillingInfo>? Bills { get; set; } = new List<BillingInfo>();

        //Relationships
        public Guid? GoalId { get; set; }
        public Goal? LinkedGoal { get; set; }
    }
}
