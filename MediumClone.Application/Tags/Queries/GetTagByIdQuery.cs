using ErrorOr;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.Common.DomainErrors;

using MediumClone.Domain.ProductCategoryEntity;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Application.Tags.Queries;
public record GetTagByIdQuery(int Id) : IRequest<ErrorOr<Tag>>;


public class GetTagIdQueryHandler : IRequestHandler<GetTagByIdQuery, ErrorOr<Tag>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetTagIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Tag>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Tags.GetByIdAsync(request.Id);
        if (tag is null)
        {
            return Errors.Common.NotFound;
        }

        return tag;

    }
}
