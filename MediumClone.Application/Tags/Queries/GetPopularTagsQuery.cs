using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.TagEntity;
using Microsoft.EntityFrameworkCore;

namespace MediumClone.Application.Tags.Queries;

public record GetPopularTagsQuery() : IRequest<List<Tag>>;

public class GetPopularTagsQueryHandler : IRequestHandler<GetPopularTagsQuery, List<Tag>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPopularTagsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Tag>> Handle(GetPopularTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _unitOfWork.Tags.GetQueryable().Include(t => t.ArticleTags)
            .OrderByDescending(x => x.ArticleTags.Count)
            .Take(10)
            .ToListAsync();

        return tags;

    }
}
