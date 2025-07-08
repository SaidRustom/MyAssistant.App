using MyAssistant.Domain.Base;
using MyAssistant.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MyAssistant.Shared.DTOs
{
    public class TaskItemDto : CreateOrUpdateTaskItemCommand ,IDto<TaskItem>
    {
        public Recurrence? Recurrence { get; set; }

        public  ICollection<EntityShare>? Shares { get; set; } = new List<EntityShare>();

        public Goal? LinkedGoal { get; set; }

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
