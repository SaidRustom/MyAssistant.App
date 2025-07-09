using MediatR;

namespace MyAssistant.Core.Features.Notifications.MarkRead
{
    public class MarkEntityNotificationsReadCommand(Guid entityId) : IRequest
    {
        public Guid EntityId { get; set; } = entityId;
    }
}
