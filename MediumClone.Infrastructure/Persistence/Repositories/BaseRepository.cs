using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Common;
using MediumClone.Infrastructure.Common;

namespace MediumClone.Infrastructure.Persistence.Repositories;
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }



    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<List<T>> GetAllAsync(string[] includes)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {

        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> match, string[] includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.SingleOrDefaultAsync(match);
    }

    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.Where(match).ToListAsync();

    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }


    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    public T Update(T entity)
    {
        _context.Update(entity);
        return entity;
    }

    public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
        return entities;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }



    public async Task<int> CountAsync()
    {
        return await _context.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
    {
        return await _context.Set<T>().CountAsync(criteria);
    }



    public Task<T> GetLatestAsync(Expression<Func<T, bool>> match, Expression<Func<T, int>> order, string[] includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }



        return query.Where(match).OrderByDescending(order).FirstOrDefaultAsync();
    }

    public async Task<T> GetFirstAsync(string[] includes = null)
    {
        //implemntaion 
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.AsNoTracking().FirstOrDefaultAsync();

    }


    public async Task<PaginatedList<T>> GetAllWithPaginationAsync(IQueryable<T> query, int pageNumber, int pageSize, string[] includes = null)
    {

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToPagedListAsync(pageNumber, pageSize);

    }

    public async Task<PaginatedList<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize, string[] includes = null)
    {
        var query = _context.Set<T>().AsQueryable();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToPagedListAsync(pageNumber, pageSize);

    }
    public async Task<PaginatedList<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize,
        Expression<Func<T, bool>> match, string[] includes = null)
    {
        var query = _context.Set<T>().AsQueryable();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.Where(match).ToPagedListAsync(pageNumber, pageSize);

    }

    public async Task<PaginatedList<T>> GetAllWithPaginationAsync(IQueryable<T> query, int pageNumber, int pageSize,
        Expression<Func<T, bool>> match, string[] includes = null)
    {
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.Where(match).ToPagedListAsync(pageNumber, pageSize);

    }

    public IQueryable<T> GetQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }
}