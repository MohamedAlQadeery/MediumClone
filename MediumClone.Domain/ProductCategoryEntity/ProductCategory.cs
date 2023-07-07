using MediumClone.Domain.Common.Entities;

namespace MediumClone.Domain.ProductCategoryEntity;

public sealed class ProductCategory : BaseEntity<int>
{


    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string? Image { get; private set; }




    private ProductCategory()
    {
    }

    private ProductCategory(string name, string description, string? image)
    {
        Name = name;
        Description = description;
        Image = image;
    }

    public static ProductCategory Create(string name, string description, string? image)
    {
        return new ProductCategory(name, description, image);
    }

    public void Update(string name, string description, string? image)
    {
        Name = name;
        Description = description;
        Image = image ?? Image;
        UpdatedDateTime = DateTime.Now;
    }









}