using FluentValidation;

namespace MediumClone.Application.ProductCategories.Commands;
public class UpdateProductCategoryCommadValidator : AbstractValidator<UpdateProductCategoryCommand>
{
    public UpdateProductCategoryCommadValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);

    }
}