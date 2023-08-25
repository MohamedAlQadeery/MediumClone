using ErrorOr;
using FluentValidation;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;

namespace MediumClone.Application.Articles.Commands;

public record DeleteArticleCommand(int Id) : IRequest<ErrorOr<Unit>>;

public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteArticleCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(x => x.Id).NotEmpty().MustAsync(BeExist).WithMessage("Article does not exist");


    }
    private async Task<bool> BeExist(int Id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Articles.GetByIdAsync(Id) != null;

    }

}


public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteArticleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var articleToDelete = await _unitOfWork.Articles.GetByIdAsync(request.Id);
        _unitOfWork.Articles.Delete(articleToDelete);

        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}
