using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Contracts.Persistence
{
    public interface INotificationRepository : IBaseAsyncRepository<Notification>
    {
        Task AddForObjAsync(Notification entity, IEntityBase obj);

        Task<ICollection<Notification>> GetObjectNotifications(Guid objectId, bool unreadOnly = false);

        Task<ICollection<Notification>> GetAllUnreadUserNotifications();
    }
}
