
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    public class NotificationDto : CreateNotificationCommand, IDto<Notification>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
