using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Contracts;
using MediatR;

namespace MyAssistant.Core.Features.Notifications.MarkRead
{
    public class MarkAllUserNotificationsAsReadCommandHandler : IRequestHandler<MarkAllUserNotificationsAsReadCommand>
    {
        private readonly ILoggedInUserService _loggedInUserService;
        private readonly INotificationRepository _repo;

        public MarkAllUserNotificationsAsReadCommandHandler(ILoggedInUserService loggedInUserService, INotificationRepository repo)
        {
            _loggedInUserService = loggedInUserService;
            _repo = repo;
        }

        public async Task Handle(MarkAllUserNotificationsAsReadCommand cmd, CancellationToken cancellationToken)
        {
            var list = await _repo.GetAllUnreadUserNotifications();
            foreach(var item in list)
            {
                item.IsRead = true;
                item.ReadAt = DateTime.Now;
            }

            await _repo.UpdateRangeAsync(list);
        }
    }
}
