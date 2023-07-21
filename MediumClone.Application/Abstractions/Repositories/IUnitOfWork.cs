using MediumClone.Domain.ProductCategoryEntity;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Application.Abstractions.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<ProductCategory> ProductCategories { get; }
    public IBaseRepository<Tag> Tags { get; }


    Task<int> SaveChangesAsync();
}