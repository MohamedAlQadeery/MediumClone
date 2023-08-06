namespace MediumClone.Application.Common;

public class CommonQueryParams
{
    public string? Search { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SortColumn { get; set; } = "id";
    public string SortOrder { get; set; } = "desc";



}