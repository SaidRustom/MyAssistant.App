using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Models;
using MyAssistant.Persistence.Repositories.Base;

namespace MyAssistant.Persistence.Repositories
{
    public class RecurrenceRepository(MyAssistantDbContext context) : BaseAsyncRepository<Recurrence>(context), IRecurrenceRepository
    {
        public override async Task<Recurrence> GetByIdAsync(Guid id)
        {
            var entity = await base.GetByIdAsync(id);
            entity.TaskItems = await _context.TaskItems.Where(x => x.RecurrenceId == id).ToListAsync();
            
            return entity;
        }
    }
}
