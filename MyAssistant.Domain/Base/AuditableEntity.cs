using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Base class for entities that require audit/history support.
    /// </summary>
    public abstract class AuditableEntity : EntityBase
    {
        /// <summary>
        /// Reference to all audit events for this entity
        /// </summary>
        [NotMapped]
        public virtual ICollection<AuditLog>? AuditLogs { get; set; } = new List<AuditLog>();
    }
}
