using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Models;
using MyAssistant.Persistence.Repositories.Base;

namespace MyAssistant.Persistence.Repositories
{
    public class NotificationRepository(MyAssistantDbContext context)
        : BaseAsyncRepository<Notification>(context), INotificationRepository
    {
        public Task<Notification> AddForObjAsync(Notification entity, IEntityBase obj)
        {
            // Find the corresponding EF entity type (may return null for unmapped types)
            var efEntityType = _context.Model.FindEntityType(obj.GetType());
            // Get the table name if found, or fallback to type name
            var tableName = efEntityType?.GetTableName() ?? entity.GetType().Name;

            entity.ObjectId = obj.Id;
            entity.ObjectType = tableName;
            entity.ActionUrl = $"{entity.ObjectType}/{entity.ObjectId}";

            return base.AddAsync(entity);   
        }
        
    }
}
