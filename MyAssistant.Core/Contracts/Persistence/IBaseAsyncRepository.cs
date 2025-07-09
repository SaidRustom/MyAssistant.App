using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Contracts.Persistence
{
    public interface IBaseAsyncRepository<T> where T : IEntityBase
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync(Guid userId);
        Task<T> AddAsync(T entity);
        Task<int> AddRangeAsync (ICollection<T> entityList);
        Task UpdateRangeAsync(ICollection<T> entityList);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(T entity);
    }
}
