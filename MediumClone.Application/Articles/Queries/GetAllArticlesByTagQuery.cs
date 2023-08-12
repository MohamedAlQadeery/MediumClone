using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Common;
using MediumClone.Domain.ArticleEntity;
using Microsoft.EntityFrameworkCore;

namespace MediumClone.Application.Articles.Queries;
public sealed record GetAllArticlesByTagQuery(string Tag, int PageNumber, int PageSize) : IRequest<PaginatedList<Article>>;

public class GetAllArticlesByTagQueryHandler : IRequestHandler<GetAllArticlesByTagQuery, PaginatedList<Article>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllArticlesByTagQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }
    public async Task<PaginatedList<Article>> Handle(GetAllArticlesByTagQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Articles.GetQueryable().Include(a => a.ArticleTags)
                    .Where(a => a.ArticleTags.Any(at => at.Tag.Name.ToLower() == request.Tag.ToLower()));

        return await _unitOfWork.Articles.GetAllWithPaginationAsync(query, request.PageNumber, request.PageSize, new string[] { "Author" });





    }
}

