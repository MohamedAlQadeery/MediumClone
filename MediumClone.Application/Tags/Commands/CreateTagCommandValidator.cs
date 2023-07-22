using FluentValidation;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Tags.Commands;

namespace MediumClone.Application.ProductCategories.Commands;
public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    private readonly IUnitOfWork _unitOfWork;


    public CreateTagCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).MustAsync(BeUniqueName).WithMessage("Tag name must be unique");

    }


    public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Tags.FindAsync(tag => tag.Name == name);
        return tag == null;

    }
}