using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Application.Articles.Queries;

public record GetAllArticlesQuery() : IRequest<IReadOnlyList<Article>>;

public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, IReadOnlyList<Article>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllArticlesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<Article>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {

        return await _unitOfWork.Articles.GetAllAsync();
    }
}

