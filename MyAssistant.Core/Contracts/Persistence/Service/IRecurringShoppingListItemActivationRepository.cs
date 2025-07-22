using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Contracts.Persistence.Service;

public interface IRecurringShoppingListItemActivationRepository : IMyAssistantServiceReporsitory
{
    Task<ICollection<ShoppingListItem>> GetItemsAsync();
    Task UpdateItems(ICollection<ShoppingListItem> items);
}
