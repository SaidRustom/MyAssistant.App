using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Base class for entities that require audit/history support.
    /// </summary>
    public abstract class AuditableEntity : EntityBase
    {
        // Reference to all audit events for this entity
        [NotMapped]
        public virtual ICollection<AuditLog>? AuditLogs { get; set; } = new List<AuditLog>();
    }
}
