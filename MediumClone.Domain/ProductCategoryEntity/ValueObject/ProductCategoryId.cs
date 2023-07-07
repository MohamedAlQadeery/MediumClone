using MediumClone.Domain.Common.Entities;

namespace MediumClone.Domain.ProductCategoryEntity;

public sealed class ProductCategoryId : EntityId<Guid>
{

    // for ef core
    public ProductCategoryId()
    {

    }

    private ProductCategoryId(Guid value) : base(value)
    {
    }

    public static ProductCategoryId Create(Guid value)
    {
        return new ProductCategoryId(value);
    }

    public static ProductCategoryId CreateUnique()
    {
        return new ProductCategoryId(Guid.NewGuid());
    }





}