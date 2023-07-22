using ErrorOr;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Application.Tags.Commands;

public record UpdateTagCommand(int Id, string Name)
: IRequest<ErrorOr<Tag>>;


public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, ErrorOr<Tag>>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateTagCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Tag>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Tags.GetByIdAsync(request.Id);
        tag.Update(request.Name);

        _unitOfWork.Tags.Update(tag);
        await _unitOfWork.SaveChangesAsync();
        return tag;


    }
}
