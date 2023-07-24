using ErrorOr;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.Common.DomainErrors;

using MediumClone.Domain.ProductCategoryEntity;
using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Application.Articles.Queries;
public record GetArticleByIdQuery(int Id) : IRequest<ErrorOr<Article>>;


public class GetArticleIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ErrorOr<Article>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetArticleIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Article>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var Article = await _unitOfWork.Articles.GetByIdAsync(request.Id);
        if (Article is null)
        {
            return Errors.Common.NotFound;
        }

        return Article;

    }
}
