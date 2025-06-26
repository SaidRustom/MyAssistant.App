using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Models;
using MyAssistant.Persistence.Repositories.Base;

namespace MyAssistant.Persistence.Repositories
{
    public class GoalRepository : BillableRepository<Goal>, IGoalRepository
    {
        public GoalRepository(MyAssistantDbContext context) : base(context) { }

    }
}
