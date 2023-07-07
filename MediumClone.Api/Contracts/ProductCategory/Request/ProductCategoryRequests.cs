namespace MediumClone.Api.Contracts.ProductCategory.Request;

public record CreateProductCategoryRequest(string Name, string Description, string? Image);
public record UpdateProductCategoryRequest(int Id, string Name, string Description, string? Image);
