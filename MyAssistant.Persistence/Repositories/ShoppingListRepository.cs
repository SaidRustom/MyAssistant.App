using System.Linq.Expressions;
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

    public override async Task<(IList<ShoppingList> Items, int TotalCount)> GetPagedListAsync(
    Guid userId,
    Expression<Func<ShoppingList, bool>> filter,
    int pageNumber,
    int pageSize)
    {
        IQueryable<ShoppingList> query = _context.Set<ShoppingList>().Where(x => x.UserId == userId);

        if (filter != null)
            query = query.Where(filter);

        int totalCount = await query.CountAsync();

        query = query.Include(x => x.Items);

        var lists = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        foreach(var list in lists)
        {
            await IncludeSharesAsync(list);

            foreach (var item in list.Items)
            {
                await IncludeAuditLogAsync(item);
            }
        }

        return (lists, totalCount);
    }
}