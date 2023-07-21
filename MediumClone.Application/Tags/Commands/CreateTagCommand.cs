using ErrorOr;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.ProductCategoryEntity;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Application.Tags.CreateTagCommand;
public record CreateTagCommand(string Name)
: IRequest<ErrorOr<Tag>>;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, ErrorOr<Tag>>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateTagCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Tag>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = Tag.Create(request.Name);
        await _unitOfWork.Tags.AddAsync(tag);
        await _unitOfWork.SaveChangesAsync();
        return tag;
    }
}
