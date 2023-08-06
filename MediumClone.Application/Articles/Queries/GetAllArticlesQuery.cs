using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Articles.Common;
using MediumClone.Application.Common;
using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Application.Articles.Queries;

public record GetAllArticlesQuery(string? Search, int PageNumber, int PageSize) : IRequest<PaginatedList<Article>>;

public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, PaginatedList<Article>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllArticlesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedList<Article>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {
        var articlesQuery = _unitOfWork.Articles.GetQueryable();
        if (!string.IsNullOrEmpty(request.Search))
        {
            articlesQuery = articlesQuery.Where(x => x.Title.Contains(request.Search));
        }

        var articles = await _unitOfWork.Articles.GetAllWithPaginationAsync(articlesQuery,
         request.PageNumber, request.PageSize, new string[] { "Author" });

        return articles;
    }
}

