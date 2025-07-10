using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

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
        Task DetachAsync(T entity);
        Task<bool> ValidateCanGetAsync(Guid entityId, Guid userId);
        Task<bool> ValidateCanEditAsync(Guid entityId, Guid userId);
        Task<PermissionType> GetUserPermissionTypeAsync(Guid entityId, Guid userId);
    }
}
