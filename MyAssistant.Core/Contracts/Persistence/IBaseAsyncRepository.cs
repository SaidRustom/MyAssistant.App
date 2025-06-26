using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Contracts.Persistence
{
    public interface IBaseAsyncRepository<T> where T : IEntityBase
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(T entity);
    }
}
