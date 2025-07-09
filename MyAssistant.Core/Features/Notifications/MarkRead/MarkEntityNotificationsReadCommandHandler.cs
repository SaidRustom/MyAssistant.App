using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;

namespace MyAssistant.Core.Features.Notifications.MarkRead
{
    public class MarkEntityNotificationsReadCommandHandler : IRequestHandler<MarkEntityNotificationsReadCommand>
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly INotificationRepository _repo;

        public MarkEntityNotificationsReadCommandHandler(ILoggedInUserService loggedInUserService, INotificationRepository repo)
        {
            _loggedInUserService = loggedInUserService;
            _repo = repo;
        }

        public async Task Handle(MarkEntityNotificationsReadCommand cmd, CancellationToken cancellationToken)
        {
            var list = await _repo.GetObjectNotifications(cmd.EntityId, true);
            foreach(var item in list)
            {
                item.IsRead = true;
                item.ReadAt = DateTime.Now;
            }

            await _repo.UpdateRangeAsync(list);
        }
    }
}
