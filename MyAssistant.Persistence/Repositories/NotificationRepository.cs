using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Models;
using MyAssistant.Persistence.Repositories.Base;

namespace MyAssistant.Persistence.Repositories
{
    public class NotificationRepository : BaseAsyncRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(MyAssistantDbContext context) : base(context) { }
    }
}
