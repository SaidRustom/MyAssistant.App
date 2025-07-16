
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    public class NotificationDto : CreateOrUpdateNotificationCommand, IDto<Notification>
    {
        public new Guid Id { get; set; }

        public Guid UserId { get; set; }

    }
}
