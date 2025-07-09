using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Models;
using MyAssistant.Persistence.Repositories.Base;

namespace MyAssistant.Persistence.Repositories
{
    public class RecurrenceRepository(MyAssistantDbContext context) : BaseAsyncRepository<Recurrence>(context), IRecurrenceRepository
    {
        
    }
}
