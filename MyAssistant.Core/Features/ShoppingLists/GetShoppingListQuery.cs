using MediatR;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.ShoppingLists;

public class GetShoppingListQuery : IRequest<ShoppingListDto>
{
    public Guid Id { get; set; }
}
