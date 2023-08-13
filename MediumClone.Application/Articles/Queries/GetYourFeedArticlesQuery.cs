using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Common;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.ArticleEntity;
using Microsoft.AspNetCore.Identity;

namespace MediumClone.Application.Articles.Queries;

public record GetYourFeedArticlesQuery(string CurrentUserId, CommonQueryParams Params) : IRequest<PaginatedList<Article>>;

public class GetYourFeedArticlesQueryHandler : IRequestHandler<GetYourFeedArticlesQuery, PaginatedList<Article>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;

    public GetYourFeedArticlesQueryHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedList<Article>> Handle(GetYourFeedArticlesQuery request, CancellationToken cancellationToken)
    {
        var currentUser = await _userManager.FindByIdAsync(request.CurrentUserId);
        var userFollowingsIds = currentUser!.Followings.Select(f => f.FollowedUserId).ToList();
        var articlesQuery = _unitOfWork.Articles.GetQueryable()
                            .Where(f => userFollowingsIds.Contains(f.AuthorId) || f.AuthorId == currentUser.Id);


        var result = await _unitOfWork.Articles.GetAllWithPaginationAsync(articlesQuery, request.Params.PageNumber, request.Params.PageSize);
        return result;
    }
}