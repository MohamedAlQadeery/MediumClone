namespace MediumClone.Application.Common;

public class PaginatedList<T>
{
    public List<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages && TotalPages > 1;




    public int TotalPages
    {
        get
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        }
    }


    public int PreviousPageNumber
    {
        get { return HasPreviousPage ? CurrentPage - 1 : 0; }
    }

    public int NextPageNumber
    {
        get { return HasNextPage ? CurrentPage + 1 : 0; }
    }
}