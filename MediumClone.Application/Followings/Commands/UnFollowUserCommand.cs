using ErrorOr;
using FluentValidation;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.FollowingEntity;
using Microsoft.AspNetCore.Identity;

namespace MediumClone.Application.Followings.Commands;
public sealed record UnFollowUserCommand(string FollowingUserId, string FollowedUserId) : IRequest<ErrorOr<Unit>>;

public class UnFollowUserCommandValidator : AbstractValidator<UnFollowUserCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    public UnFollowUserCommandValidator(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        RuleFor(x => x.FollowingUserId).NotEmpty().MustAsync(UserIdBeExist).WithMessage("User with this id doesn't exist");
        RuleFor(x => x.FollowedUserId).NotEmpty().MustAsync(UserIdBeExist).WithMessage("User with this id doesn't exist");
        RuleFor(x => x).Must(UserCantUnFollowHimself).WithMessage("User can't unfollow himself");
        RuleFor(x => x).MustAsync(UserIsInFollowingList).WithMessage("User is not following this user");


    }

    public async Task<bool> UserIdBeExist(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user is not null;

    }

    public async Task<bool> UserIsInFollowingList(UnFollowUserCommand command, CancellationToken cancellationToken)
    {
        var followingExist = await _unitOfWork.Followings
        .FindAsync(f => f.FollowingUserId == command.FollowingUserId && f.FollowedUserId == command.FollowedUserId);

        return followingExist is not null;
    }

    public bool UserCantUnFollowHimself(UnFollowUserCommand command)
    {
        return command.FollowedUserId != command.FollowingUserId;
    }
}

public class UnFollowUserCommandHandler : IRequestHandler<UnFollowUserCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    public UnFollowUserCommandHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    public async Task<ErrorOr<Unit>> Handle(UnFollowUserCommand request, CancellationToken cancellationToken)
    {
        var userFollowing = await _userManager.FindByIdAsync(request.FollowingUserId);
        var followingToRemove = userFollowing?.Followings.FirstOrDefault(f => f.FollowedUserId == request.FollowedUserId);

        // userFollowing!.UnfollowUser(followingToRemove!);
        _unitOfWork.Followings.Delete(followingToRemove!);

        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;

    }
}
