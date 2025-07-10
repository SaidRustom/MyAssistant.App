using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Models;
using MyAssistant.Persistence.Repositories.Base;

namespace MyAssistant.Persistence.Repositories;

public class ShoppingListRepository(MyAssistantDbContext context) : BaseAsyncRepository<ShoppingList>(context), IShoppingListRepository
{
    /// <summary>
    /// Add the related ShoppingListItems
    /// </summary>
    public override async Task<ShoppingList> GetByIdAsync(Guid id)
    {
        var list = await base.GetByIdAsync(id);
        list.Items = await _context.ShoppingListItems
            .Where(x => x.ShoppingListId == list.Id)
            .ToListAsync();

        foreach(var item in list.Items)
            await IncludeAuditLogAsync(item);
        
        return list;
    }
}