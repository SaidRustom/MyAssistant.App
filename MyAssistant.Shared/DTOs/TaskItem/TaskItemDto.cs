using MyAssistant.Domain.Base;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    {
        public new Guid Id { get; set; }

        public Guid UserId { get; set; }

        public bool IsEditable { get; set; }

        public Recurrence? Recurrence { get; set; }

        public PermissionType PermissionType { get; set; } = default!;

        public  ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

        public Goal? LinkedGoal { get; set; }

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
