using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Application.Tags.Queries;

public record GetAllTagsQuery() : IRequest<IReadOnlyList<Tag>>;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, IReadOnlyList<Tag>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTagsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<Tag>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {

        return await _unitOfWork.Tags.GetAllAsync();
    }
}

