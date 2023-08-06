using System.Linq.Expressions;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Articles.Common;
using MediumClone.Application.Common;
using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Application.Articles.Queries;

public record GetAllArticlesQuery(CommonQueryParams Params) : IRequest<PaginatedList<Article>>;

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
        if (!string.IsNullOrEmpty(request.Params.Search))
        {
            articlesQuery = articlesQuery.Where(x => x.Title.Contains(request.Params.Search));
        }


        if (request.Params.SortOrder?.ToLower() == "desc")
        {
            articlesQuery = articlesQuery.OrderByDescending(GetSortColumn(request.Params.SortColumn));
        }
        else
        {
            articlesQuery = articlesQuery.OrderBy(GetSortColumn(request.Params.SortColumn));
        }


        var articles = await _unitOfWork.Articles.GetAllWithPaginationAsync(articlesQuery,
         request.Params.PageNumber, request.Params.PageSize, new string[] { "Author" });

        return articles;
    }


    private static Expression<Func<Article, object>> GetSortColumn(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "title" => x => x.Title,
            "createdDateTime" => x => x.CreatedDateTime,
            _ => x => x.Id
        };
    }
}

