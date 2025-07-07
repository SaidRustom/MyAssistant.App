using Microsoft.EntityFrameworkCore;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Persistence.Repositories.Service
{
    public class MyAssistantServiceReporsitory(MyAssistantDbContext context) 
    {
        protected readonly MyAssistantDbContext _context = context;

        public virtual async Task<MyAssistantServiceLog> LogServiceStartedAsync(MyAssistantServiceLog entity)
        {
            entity.StartTime = DateTime.Now;
            await _context.Set<MyAssistantServiceLog>().AddAsync(entity);
            await _context.SurpassAuditAndSaveAsync();
            return entity;
        }

        public virtual async Task LogServiceEndedAsync(MyAssistantServiceLog entity)
        {
            entity.EndTime = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SurpassAuditAndSaveAsync();
        }
    }
}
