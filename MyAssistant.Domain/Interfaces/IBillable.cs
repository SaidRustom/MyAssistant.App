using MyAssistant.Domain.Models;

namespace MyAssistant.Domain.Interfaces
{
    public interface IBillable
    {
        ICollection<BillingInfo>? Billings { get; set; }
    }
}
