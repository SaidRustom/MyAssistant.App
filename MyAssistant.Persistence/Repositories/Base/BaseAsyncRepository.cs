using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Persistence.Repositories.Base
{
    public class BaseAsyncRepository<T> : IBaseAsyncRepository<T> where T : class, IEntityBase
    {
        protected readonly MyAssistantDbContext _context;

        public BaseAsyncRepository(MyAssistantDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
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
    }
}
