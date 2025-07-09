using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    [Table("Goal")]
    public class Goal : AuditableEntity, IShareable<Goal>, IBillable<Goal>
    { //TODO: add progress features
        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? TargetDate { get; set; }

        public bool IsAchieved { get; set; }

        public ICollection<TaskItem> LinkedTasks { get; set; } = new List<TaskItem>();
        public ICollection<Habit> LinkedHabits { get; set; } = new List<Habit>();

        public ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

        public ICollection<BillingInfo>? Bills { get; set; } = new List<BillingInfo>();
    }
}
