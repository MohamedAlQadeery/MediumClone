using ErrorOr;
namespace MediumClone.Domain.Common.DomainErrors;
public static partial class Errors
{
    public static class Product
    {

        public static Error MustHaveSpecification =>
         Error.Validation(code: "MustHaveSpecification", description: "Product must have at least one specification");

        //category not found
        public static Error CategoryNotFound =>
        Error.Validation(code: "CategoryNotFound", description: "Category not found");

        //product not found
        public static Error ProductNotFound =>
        Error.Validation(code: "ProductNotFound", description: "Product not found");
    }
}