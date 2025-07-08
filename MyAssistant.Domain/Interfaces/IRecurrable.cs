
using MyAssistant.Domain.Models;

namespace MyAssistant.Domain.Interfaces
{
    public interface IRecurrable
    {
        Guid? RecurrenceId { get; set; }
        Recurrence? Recurrence { get; set; }
    }
}
