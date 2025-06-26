using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Core.Contracts.Persistence
{
    public interface IShareableRepository<T> : IBaseAsyncRepository<T> where T : IShareable<T>, IEntityBase
    {
        Task ShareWithOtherUserAsync(T obj, Guid userId, PermissionType permissionType);

        Task CancelShareWithOtherUserAsync(T obj, EntityShare share);
    }
}
