using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Repositories.Base
{
    /// <summary>
    /// Implements IShrearable as all current IBillable objects implement IShareable, create another class in the future if needed
    /// </summary>
    public class BillableRepository<T> : BaseAsyncRepository<T> where T : class, IBillable<T>, IShareable<T>, IEntityBase
    {
        public BillableRepository(MyAssistantDbContext context) : base(context) { }

        /// <summary>
        /// Returns all billing info - could be optimized when needed..
        /// </summary>
        public virtual new async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().Include(x => x.Bills).Include(x => x.Shares).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns the whole obj in case needed for calculation purpuses etc, could be optimized..
        /// </summary>
        public virtual async Task<T> AddBillAsync(T obj, BillingInfo billingInfo)
        {
            billingInfo.ParentEntityId = obj.Id;
            billingInfo.ParentEntityType = typeof(T).Name;

            await _context.Set<BillingInfo>().AddAsync(billingInfo);

            return await _context.Set<T>()
                .Include(x => x.Bills)
                .Include(x => x.Shares)
                .Where(x => x.Id == obj.Id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns all the billing info for all the objects, could/should be optimized..
        /// </summary>
        public virtual new async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .Include(x => x.Bills)
                .Include(x => x.Shares)
                .ToListAsync();
        }
    }
}
