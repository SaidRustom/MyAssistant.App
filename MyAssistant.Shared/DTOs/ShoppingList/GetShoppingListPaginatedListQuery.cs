using MediatR;
using MyAssistant.Core.Responses;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.ShoppingLists.GetList
{
    public class GetShoppingListPaginatedListQuery : IRequest<PaginatedList<ShoppingListDto>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public GetShoppingListPaginatedListQuery(int page, int countPerPage)
        {
            PageNumber = page;
            PageSize = countPerPage;
        }
    }
}
