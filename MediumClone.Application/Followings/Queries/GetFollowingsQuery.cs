using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Common;
using MediumClone.Domain.FollowingEntity;

namespace MediumClone.Application.Followings.Queries;

public record GetFollowingsQuery(string CurrentUserId, CommonQueryParams QueryParams) : IRequest<PaginatedList<Following>>;

public class GetFollowingsQueryHandler : IRequestHandler<GetFollowingsQuery, PaginatedList<Following>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFollowingsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedList<Following>> Handle(GetFollowingsQuery request, CancellationToken cancellationToken)
    {

        var query = _unitOfWork.Followings.GetQueryable();
        query = query.Where(f => f.FollowingUserId == request.CurrentUserId); ;

        var followings = await _unitOfWork.Followings.
        GetAllWithPaginationAsync(query, request.QueryParams.PageNumber, request.QueryParams.PageSize, new string[] { "FollowedUser" });

        return followings;

    }
}