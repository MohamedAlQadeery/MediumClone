using ErrorOr;
using FluentValidation;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.FollowingEntity;
using Microsoft.AspNetCore.Identity;

namespace MediumClone.Application.Followings.Commands;
public sealed record FollowUserCommand(string FollowingUserId, string FollowedUserId) : IRequest<ErrorOr<Following>>;

public class FollowUserCommandValidator : AbstractValidator<FollowUserCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    public FollowUserCommandValidator(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        RuleFor(x => x.FollowingUserId).NotEmpty().MustAsync(UserIdBeExist).WithMessage("User with this id doesn't exist");
        RuleFor(x => x.FollowedUserId).NotEmpty().MustAsync(UserIdBeExist).WithMessage("User with this id doesn't exist");
        RuleFor(x => x).Must(UserCantFollowHimself).WithMessage("User can't follow himself");
        RuleFor(x => x).MustAsync(UserIsNotAlreadyFollowed).WithMessage("User is already followed");


    }

    public async Task<bool> UserIdBeExist(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user is not null;

    }

    public async Task<bool> UserIsNotAlreadyFollowed(FollowUserCommand command, CancellationToken cancellationToken)
    {
        var followingExist = await _unitOfWork.Followings
        .FindAsync(f => f.FollowingUserId == command.FollowingUserId && f.FollowedUserId == command.FollowedUserId);

        return followingExist is null;
    }

    public bool UserCantFollowHimself(FollowUserCommand command)
    {
        return command.FollowedUserId != command.FollowingUserId;
    }
}

public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, ErrorOr<Following>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    public FollowUserCommandHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    public async Task<ErrorOr<Following>> Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var userFollowing = await _userManager.FindByIdAsync(request.FollowingUserId);
        var result = userFollowing!.FollowUser(request.FollowedUserId);
        await _unitOfWork.SaveChangesAsync();
        return result;

    }
}
