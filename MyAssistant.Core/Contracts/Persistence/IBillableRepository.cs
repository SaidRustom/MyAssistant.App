using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Contracts.Persistence
{
    public interface IBillableRepository<T> : IBaseAsyncRepository<T> where T : IBillable<T>, IEntityBase
    {
        public Task<T> AddBillAsync(T obj, BillingInfo billingInfo);
    }
}
