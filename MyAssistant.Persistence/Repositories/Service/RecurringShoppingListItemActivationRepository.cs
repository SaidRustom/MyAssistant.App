using System.Threading;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts.Persistence.Service;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;

namespace MyAssistant.Persistence.Repositories.Service;

public class RecurringShoppingListItemActivationRepository(MyAssistantDbContext context) : 
    MyAssistantServiceReporsitory(context), 
    IRecurringShoppingListItemActivationRepository
{
    public async Task<ICollection<ShoppingListItem>> GetItemsAsync()
    {
        return await context.ShoppingListItems
                .Where(x => x.IsRecurring
                    && x.NextOccurrenceDate.HasValue
                    && x.NextOccurrenceDate.Value < DateTime.Now
                    && !x.IsActive)
                .ToListAsync();
    }

    public async Task UpdateItems(ICollection<ShoppingListItem> items)
    {   
        _context.UpdateRange(items);
        await _context.SaveServiceChangesAsync(MyAssistantServiceTypeList.Get(MyAssistantServiceType.RecurringShoppingListItemActivationService));   
    }
}
