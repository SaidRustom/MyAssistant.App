using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Models;
using MyAssistant.Persistence.Repositories.Base;

namespace MyAssistant.Persistence.Repositories
{
    public class NotificationRepository(MyAssistantDbContext context, ILoggedInUserService loggedInUserService)
        : BaseAsyncRepository<Notification>(context), INotificationRepository
    {
        private readonly ILoggedInUserService _loggedInUserService = loggedInUserService;

        public async Task AddForObjAsync(Notification entity, IEntityBase obj)
        {
            // Find the corresponding EF entity type (may return null for unmapped types)
            var efEntityType = _context.Model.FindEntityType(obj.GetType());
            // Get the table name if found, or fallback to type name
            var tableName = efEntityType?.GetTableName() ?? entity.GetType().Name;

            entity.ObjectId = obj.Id;
            entity.ObjectType = tableName;
            entity.ActionUrl = $"{entity.ObjectType}/{entity.ObjectId}";

            bool notificationAlreadySent = await _context.Notifications.AnyAsync(x => x.ActionUrl == entity.ActionUrl && !x.IsRead && x.SentAt.AddDays(2) > DateTime.Now);

            if (!notificationAlreadySent)
            {
                await base.AddAsync(entity);
            }   
        }

        public async Task<ICollection<Notification>> GetObjectNotifications(Guid objectId, bool unreadOnly = false)
        {
            if(unreadOnly)
                return await _context.Notifications.Where(x => x.UserId == _loggedInUserService.UserId && x.ObjectId == objectId && !x.IsRead).ToListAsync();

            return await _context.Notifications.Where(x => x.UserId == _loggedInUserService.UserId && x.ObjectId == objectId).ToListAsync();
        }

        public async Task<ICollection<Notification>> GetAllUnreadUserNotifications()
        {
            var list = await _context.Notifications.Where(x => x.UserId == _loggedInUserService.UserId).ToListAsync();
            return list;
        }
        
    }
}
