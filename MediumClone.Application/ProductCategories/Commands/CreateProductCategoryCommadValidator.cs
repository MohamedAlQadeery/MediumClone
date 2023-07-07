using FluentValidation;

namespace MediumClone.Application.ProductCategories.Commands;
public class CreateProductCategoryCommadValidator : AbstractValidator<CreateProductCategoryCommad>
{
    public CreateProductCategoryCommadValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);

    }
}