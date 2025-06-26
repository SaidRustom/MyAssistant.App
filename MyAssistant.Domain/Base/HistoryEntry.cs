using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Stores changes made in a specific Audit entry (property, old value, new value)
    /// </summary>
    [Table("historyEntry")]
    public class HistoryEntry : EntityBase
    {
        [Required]
        public Guid AuditLogId { get; set; }

        public virtual AuditLog AuditLog { get; set; } = default!;

        [Required]
        [StringLength(255)]
        public string PropertyName { get; set; } = default!;

        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

        public HistoryEntry() { }

        /// <summary>
        /// Requires an instance of AuditLog
        /// </summary>
        /// <param name="auditLog"></param>
        public HistoryEntry(AuditLog auditLog)
        {
            AuditLogId = auditLog.Id;
            AuditLog = auditLog;
        }
    }
}
