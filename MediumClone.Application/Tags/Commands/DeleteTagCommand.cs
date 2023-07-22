using ErrorOr;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.Common.DomainErrors;
using MediumClone.Domain.ProductCategoryEntity;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Application.Tags.Commands;
public record DeleteTagCommand(int id)
: IRequest<ErrorOr<Unit>>;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteTagCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Tags.GetByIdAsync(request.id);
        if (tag is null)
        {
            return Errors.Common.NotFound;
        }

        _unitOfWork.Tags.Delete(tag);
        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}
