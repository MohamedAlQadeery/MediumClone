using FluentValidation;
using MediumClone.Application.Tags.CreateTagCommand;

namespace MediumClone.Application.ProductCategories.Commands;
public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);

    }
}