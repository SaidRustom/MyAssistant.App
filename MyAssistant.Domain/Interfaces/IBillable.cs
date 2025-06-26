using MyAssistant.Domain.Models;

namespace MyAssistant.Domain.Interfaces
{
    public interface IBillable<T> : IEntityBase where T : IEntityBase
    {
        ICollection<BillingInfo>? Bills { get; set; }
    }
}
