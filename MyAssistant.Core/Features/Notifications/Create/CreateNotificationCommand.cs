using MediatR;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Core.Features.Notifications.Create
{
    public class CreateNotificationCommand : IRequest
    {
        public IEntityBase Entity { get; set; }

        public Guid TargetUserId { get; set; }

        public string Title { get; set; }

        public string? Message { get; set; }

        public CreateNotificationCommand(IEntityBase entity, Guid targetUserId, string title, string? message = null)
        {
            Entity = entity;
            TargetUserId = targetUserId;
            Title = title;
            Message = message;
        }
    }
}
