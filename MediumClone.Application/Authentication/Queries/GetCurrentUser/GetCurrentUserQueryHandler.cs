using System.Linq;
using System.Security.Claims;
using ErrorOr;
using MediatR;
using MediumClone.Application.Authentication.Common;
using MediumClone.Application.Common.Interfaces;
using MediumClone.Domain.AppUserEntity;
using Microsoft.AspNetCore.Identity;

namespace MediumClone.Application.Authentication.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ErrorOr<AuthenticationResult>>
{
    private readonly UserManager<AppUser> _userManager;
    public GetCurrentUserQueryHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;

    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = request.HttpContext.User;
        var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return Error.Failure(code: "IdentityError", description: "No user id was found");
        }


        var appUser = await _userManager.FindByIdAsync(userId);

        if (appUser is null)
        {
            return Error.Failure(code: "IdentityError", description: "User not found");
        }

        var token = request.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();


        return new AuthenticationResult(appUser, token!);




    }
}