using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.ProductCategoryEntity;

namespace MediumClone.Infrastructure.Persistence.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IBaseRepository<ProductCategory> ProductCategories { get; private set; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        ProductCategories = new BaseRepository<ProductCategory>(_context);


    }



    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}