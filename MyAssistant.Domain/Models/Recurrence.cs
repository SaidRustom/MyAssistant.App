using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Domain.Models
{
    /// <summary>
    /// Represents a recurring schedule entity used to define repeated events or tasks.
    /// </summary>
    [Table("Recurrence")]
    public class Recurrence : AuditableEntity
    {
        [Required]
        public string Title { get; set; } = default!;

        public string? Description { get; set; } 

        public int Interval { get; set; } // Every N days/weeks/months etc..

        [Range(1, 7)]
        public int RecurrenceTypeCode { get; set; }

        [Range(1, 5)]
        public int DefaultPriority { get; set; }

        public RecurrenceType RecurrenceType { get; set; } = default!;
        
        public virtual ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>(); 

        /// <summary>
        /// in case the task should be scheduled at the same time every ocurrence
        /// </summary>
        public TimeSpan? Time { get; set; }
        public int LengthInMinutes { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now.Date;
        public DateTime? EndDate { get; set; }

        public Recurrence () { }

        public Recurrence(int interval, RecurrenceType recurrenceType, DateTime? EndDate = null)
        {
            Interval = interval;
            RecurrenceType = recurrenceType;
            RecurrenceTypeCode = recurrenceType.Code;
        }
    }
}
