using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Persistence.Repositories.Base
{
    public class BaseAsyncRepository<T>(MyAssistantDbContext context) : IBaseAsyncRepository<T>
        where T : class, IEntityBase
    {
        protected readonly MyAssistantDbContext _context = context;

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            var obj = (await _context.Set<T>().FindAsync(id))!;
            
            if (obj is AuditableEntity auditable)
                await IncludeAuditLogAsync(auditable);
            if(obj is IShareable<T> shareable)
                await IncludeSharesAsync(shareable);
            //TODO: Add Billable

            return obj;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            var list = await _context.Set<T>().ToListAsync();

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

        public virtual async Task<T> AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid();

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsAsync(T entity)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id == entity.Id);
        }
        
        #region Helper Methods
        
        /// <summary>
        /// Add the audit logs of the AuditableEntity
        /// </summary>
        private async Task<AuditableEntity> IncludeAuditLogAsync(AuditableEntity auditable)
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
        private async Task<IShareable<T>> IncludeSharesAsync(IShareable<T> shareable)
        {
            shareable.Shares = await _context.EntityShares
                .Where(x => x.EntityId == shareable.Id)
                .ToListAsync();

            return shareable;
        }

        #endregion       
    }
}
