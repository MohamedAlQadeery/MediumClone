using FluentValidation;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Tags.Commands;

namespace MediumClone.Application.Tags.Commands;
public class UpdateTagCommandValidator : AbstractValidator<UpdateTagCommand>
{
    private readonly IUnitOfWork _unitOfWork;


    public UpdateTagCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.Id).MustAsync(BeExist).WithMessage("The tag does not exist");
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        //RuleFor(x => x.Name).MustAsync(BeUniqueName).WithMessage("Tag name must be unique");
        RuleFor(x => x).MustAsync(BeUniqueName).WithMessage("Tag name must be unique");

    }


    public async Task<bool> BeUniqueName(UpdateTagCommand command, CancellationToken cancellationToken)
    {
        //ignore the current tag name

        var tag = await _unitOfWork.Tags.FindAsync(tag => tag.Name == command.Name && tag.Id != command.Id);
        return tag == null;

    }

    public async Task<bool> BeExist(int id, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Tags.GetByIdAsync(id);
        return tag is not null;

    }
}