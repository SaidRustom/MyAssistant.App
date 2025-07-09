using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Features.Notifications.Create
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand>
    {
        private INotificationRepository _repo;

        public CreateNotificationCommandHandler(INotificationRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(CreateNotificationCommand cmd, CancellationToken cancellationToken)
        {
            Notification notification = new()
            {
                UserId = cmd.TargetUserId,
                Title = cmd.Title,
                Message = cmd.Message,
            };

            if (notification.UserId.Equals(Guid.Empty) || string.IsNullOrEmpty(notification.Title) || cmd.Entity == null)
                throw new InvalidOperationException("Title and TargetUserId are required to add new notification");

            await _repo.AddForObjAsync(notification, cmd.Entity);
        }
    }
}
