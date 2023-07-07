namespace MediumClone.Api.Contracts.ProductCategory;

public record ProductCategoryResponse(
    int Id,
    string Name,
    string Description,
    string Image,
    List<int> ProductsIds
);
