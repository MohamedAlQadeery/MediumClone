using Microsoft.EntityFrameworkCore;
using MediumClone.Application.Common;

namespace MediumClone.Infrastructure.Common;

public static class IQueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        var totalItems = await query.CountAsync();
        var items = await query.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();
        return new PaginatedList<T>
        {
            Items = items,
            TotalItems = totalItems,
            CurrentPage = pageNumber,
            ItemsPerPage = pageSize
        };
    }
}