using MyAssistant.Domain.Base;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    public class TaskItemDto : CreateOrUpdateTaskItemCommand, IDto<TaskItem>, IShareableDto<TaskItem>
    {
        public new Guid Id { get; set; }

        public Guid UserId { get; set; }
        
        public Recurrence? Recurrence { get; set; }

        public LookupDto PermissionType { get; set; }

        public  ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

        public Goal? LinkedGoal { get; set; }

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
