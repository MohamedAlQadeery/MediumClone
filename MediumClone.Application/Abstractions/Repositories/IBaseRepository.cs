using System.Linq.Expressions;
using MediumClone.Application.Common;

namespace MediumClone.Application.Abstractions.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);

    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(string[] includes);

    Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null);


    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);


    T Update(T entity);
    IEnumerable<T> UpdateRange(IEnumerable<T> entities);

    void Delete(T entity);

    Task<int> CountAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> criteria);

    Task<T> GetLatestAsync(Expression<Func<T, bool>> match, Expression<Func<T, int>> order, string[] includes = null);
    Task<T> GetFirstAsync(string[] includes = null);

    Task<PaginatedList<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize, string[] includes = null);
    Task<PaginatedList<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> match, string[] includes = null);

}