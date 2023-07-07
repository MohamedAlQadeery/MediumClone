using MediumClone.Domain.ProductCategoryEntity;

namespace MediumClone.Application.Abstractions.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<ProductCategory> ProductCategories { get; }


    Task<int> SaveChangesAsync();
}