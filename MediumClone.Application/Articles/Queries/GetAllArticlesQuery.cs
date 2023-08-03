using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Articles.Common;
using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Application.Articles.Queries;

public record GetAllArticlesQuery() : IRequest<ArticlesResult>;

public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, ArticlesResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllArticlesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ArticlesResult> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {


        var articles = await _unitOfWork.Articles.GetAllAsync(new string[] { "Author" });
        var articlesCount = await _unitOfWork.Articles.CountAsync();
        return new ArticlesResult(articles, articlesCount);
    }
}

