
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    public class NotificationDto : CreateOrUpdateNotificationCommand, IDto<Notification>
    {
        public Guid UserId { get; set; }

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
