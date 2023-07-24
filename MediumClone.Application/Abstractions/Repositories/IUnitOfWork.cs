using MediumClone.Domain.ArticleEntity;
using MediumClone.Domain.ArticleTagEntity;
using MediumClone.Domain.ProductCategoryEntity;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Application.Abstractions.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IBaseRepository<ProductCategory> ProductCategories { get; }
    public IBaseRepository<Tag> Tags { get; }
    public IBaseRepository<Article> Articles { get; }
    public IBaseRepository<ArticleTag> ArticleTags { get; }


    Task<int> SaveChangesAsync();
}