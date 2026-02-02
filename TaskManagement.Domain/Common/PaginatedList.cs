namespace TaskManagement.Domain.Common;

public class PaginatedList<T>
{
    public List<T> Items { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public int PageNumber { get; }

    public PaginatedList(List<T> items, int totalCount, int totalPages, int pageNumber)
    {
        Items = items;
        TotalCount = totalCount;
        TotalPages = totalPages;
        PageNumber = pageNumber;
    }

    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
