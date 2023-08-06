using Microsoft.EntityFrameworkCore;

namespace MediumClone.Application.Common;

// public class PagedList<T>
// {
//     public List<T> Items { get; }
//     public int TotalCount { get; }
//     public int CurrentPage { get; }
//     public int PageSize { get; }

//     public bool HasNextPage => CurrentPage * PageSize < TotalCount;
//     public bool HasPreviousPage => CurrentPage > 1;

//     public PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
//     {
//         Items = items;
//         TotalCount = totalCount;
//         CurrentPage = currentPage;
//         PageSize = pageSize;
//     }


//     public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
//     {
//         var count = await source.CountAsync();
//         var items = await source.Skip((pageNumber - 1) * pageSize)
//                                .Take(pageSize)
//                                .ToListAsync();
//         return new PagedList<T>(items, count, pageNumber, pageSize);
//     }
// }