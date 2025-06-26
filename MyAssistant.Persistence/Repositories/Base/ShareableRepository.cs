using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Persistence.Repositories.Base
{
    public class ShareableRepository<T> : BaseAsyncRepository<T> where T : class, IShareable<T>, IEntityBase
    {
        public ShareableRepository(MyAssistantDbContext context) : base(context) { }


        public virtual new async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().Include(x => x.Shares).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<T> ShareWithOtherUserAsync(T obj, Guid otherUserId, PermissionType permissionType)
        {
            EntityShare share = new(obj, otherUserId, permissionType);

            await _context.Set<EntityShare>().AddAsync(share);

            return await _context.Set<T>().Include(x => x.Shares).Where(x => x.Id == obj.Id).FirstOrDefaultAsync();
        }

        public virtual new async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().Include(x => x.Shares).ToListAsync();
        }
    }
}
