using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Application.Common;
using MediumClone.Domain.FollowingEntity;

namespace MediumClone.Application.Followings.Queries;

public record GetFollowersQuery(string CurrentUserId, CommonQueryParams QueryParams) : IRequest<PaginatedList<Following>>;

public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, PaginatedList<Following>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFollowersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedList<Following>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
    {

        var query = _unitOfWork.Followings.GetQueryable();
        query = query.Where(f => f.FollowedUserId == request.CurrentUserId); ;

        var followings = await _unitOfWork.Followings.
        GetAllWithPaginationAsync(query, request.QueryParams.PageNumber, request.QueryParams.PageSize, new string[] { "FollowingUser" });

        return followings;

    }
}