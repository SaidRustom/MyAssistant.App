using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Persistence.Repositories.Base
{
    public class BaseAsyncRepository<T>(MyAssistantDbContext context) : IBaseAsyncRepository<T>
        where T : class, IEntityBase
    {
        protected readonly MyAssistantDbContext _context = context;
        
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var obj = await _context.Set<T>().FindAsync(id)!;
            
            if (obj is AuditableEntity auditable)
                await IncludeAuditLogAsync(auditable);
            if(obj is IShareable<T> shareable)
                await IncludeSharesAsync(shareable);
            //TODO: Add Billable

            return obj;
        }

        public virtual async Task<List<T>> GetAllAsync(Guid userId)
        {
            var list = await _context.Set<T>().Where(x => x.UserId == userId).ToListAsync();

            if (typeof(AuditableEntity).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in list)
                    await IncludeAuditLogAsync((item as AuditableEntity)!);
            }

            if (typeof(IShareable<T>).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in list)
                    await IncludeSharesAsync((item as IShareable<T>)!);
            }

            return list;
        }
        
        public virtual async Task<(IList<T> Items, int TotalCount)> GetPagedListAsync(
            Guid userId,
            Expression<Func<T, bool>> filter,
            int pageNumber,
            int pageSize)
        {
            IQueryable<T> query = _context.Set<T>().Where(x => x.UserId == userId);

            if (filter != null)
                query = query.Where(filter);

            int totalCount = await query.CountAsync();

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            if (typeof(AuditableEntity).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in items)
                    await IncludeAuditLogAsync((item as AuditableEntity)!);

                query = query.OrderByDescending(x => (x as AuditableEntity)!.AuditLogs.AsEnumerable().Max(l => l.DateTime));
            }

            if (typeof(IShareable<T>).IsAssignableFrom(typeof(T)))
            {
                foreach (var item in items)
                    await IncludeSharesAsync((item as IShareable<T>)!);
            }

            return (items, totalCount);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid();

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<int> AddRangeAsync(ICollection<T> entityList)
        {
            foreach(var entity in entityList)
                entity.Id = Guid.NewGuid();

            await _context.Set<T>().AddRangeAsync(entityList);
            await _context.SaveChangesAsync();

            return entityList.Count;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(ICollection<T> entityList)
        {
            _context.UpdateRange(entityList);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsAsync(T? entity)
        {
            if(entity == null)
                return false;
            else
                return await _context.Set<T>().AnyAsync(x => x.Id == entity.Id);
        }

        public async Task DetachAsync(T entity)
        {
            var existingTrackedItem = context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => e.Entity.Id == entity.Id);
            
            if (existingTrackedItem != null)
            {
                existingTrackedItem.State = EntityState.Detached;
            }
        }

        /// <summary>
        /// Wheather the user can read the entity
        /// </summary>
        public async Task<bool> ValidateCanGetAsync(Guid entityId, Guid userId)
        {
            return (await GetUserPermissionTypeAsync(entityId, userId)) is not null;
        }

        public async Task<bool> ValidateCanEditAsync(Guid entityId, Guid userId)
        {
            var permissionTypeCode = (await GetUserPermissionTypeAsync(entityId, userId)).Code;
            return permissionTypeCode != PermissionType.Read;
        }

        public async Task<PermissionType> GetUserPermissionTypeAsync(Guid entityId, Guid userId)
        {
            var entity = await _context.Set<T>().FindAsync(entityId);
            await DetachAsync(entity);

            if (entity == null)
                throw new Exception($"{typeof(T).Name} - {entityId} does not exist");

            if (entity.UserId.Equals(userId)) //The owner of the entity - all permissions
                return PermissionTypeList.Get(PermissionType.ReadWriteDelete);

            var shareable = entity
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IShareable<>));

            if (shareable == null) //Not a shareable type, throw an exception
                throw new UnauthorizedAccessException();

            var share = await _context.EntityShares
                .Where(x => x.SharedWithUserId == userId && x.EntityId == entity.Id)
                .FirstOrDefaultAsync();

            if (share == null)
                throw new UnauthorizedAccessException();

            return share.PermissionType;
        }

        #region Helper Methods
        
        /// <summary>
        /// Add the audit logs of the AuditableEntity
        /// </summary>
        protected async Task<AuditableEntity> IncludeAuditLogAsync(AuditableEntity auditable)
        {
            auditable.AuditLogs = await _context.AuditLogs
                .Where(x => x.EntityId == auditable.Id)
                .Include(x => x.HistoryEntries)
                .OrderByDescending(x => x.EventDate)
                .ToListAsync();
            
            return auditable;
        }

        /// <summary>
        /// Add the Shares of the IShareable entity
        /// </summary>
        protected async Task<IShareable<T>> IncludeSharesAsync(IShareable<T> shareable)
        {
            shareable.Shares = await _context.EntityShares
                .Where(x => x.EntityId == shareable.Id)
                .ToListAsync();

            return shareable;
        }
        
        #endregion       
    }
}
