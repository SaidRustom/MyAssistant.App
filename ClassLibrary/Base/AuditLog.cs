using System.ComponentModel.DataAnnotations;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Represents an audit event for an entity (user/date/action).
    /// </summary>
    public class AuditLog : EntityBase
    {
        [Required]
        public Guid EntityId { get; set; }

        [Required]
        [StringLength(100)]
        public string EntityType { get; set; } = default!;

        [Required]
        public Guid UserId { get; set; } = default!;

        [Required]
        public DateTime EventDate { get; set; }

        [StringLength(100)]
        public string? Action { get; set; } // e.g. "Create", "Update", "Delete"

        public virtual ICollection<HistoryEntry> HistoryEntries { get; set; } = new List<HistoryEntry>();

        /// <summary>
        /// Creating a new instance requires an EntityBase
        /// </summary>
        /// <param name="entity"></param>
        public AuditLog(EntityBase entity)
        {
            EntityId = entity.Id;
            EntityType = entity.GetType().Name;
        }
    }
}
