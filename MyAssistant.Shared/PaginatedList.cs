namespace MyAssistant.Core.Responses;

public class PaginatedList<T>
{
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
    public List<T> Items { get; }

    public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}