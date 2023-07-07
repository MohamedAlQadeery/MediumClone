using ErrorOr;
using MediatR;
using MediumClone.Application.Common.Interfaces;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.AppUserEntity.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MediumClone.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IPublisher _mediatr;


    public IdentityService(
        UserManager<AppUser> userManager,
        IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IPublisher mediatr,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _mediatr = mediatr;
        _roleManager = roleManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<ErrorOr<AppUser>> CreateUserAsync(AppUser user, string password)
    {


        var roleExist = await _roleManager.RoleExistsAsync(AppUserRole.FromValue(user.AppUserRole).Name);
        if (!roleExist)
        {
            return Error.Failure(code: "IdentityError", description: "Role not found");
        }

        var result = await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, AppUserRole.FromValue(user.AppUserRole).Name);


        if (result.Succeeded)
        {
            // await _mediatr.Publish(new UserCreated(applicationUser.Id, AppUserRole.FromValue(user.AppUserRole).Name));


            return user;
        }


        return Error.Failure(code: "IdentityError", description: result.Errors.First().Description);

    }

    public async Task<ErrorOr<AppUser>> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return Error.Failure(code: "IdentityError", description: "User not found");
        }

        var result = await _userManager.CheckPasswordAsync(user, password);


        return result
            ? user
            : Error.Failure(code: "IdentityError", description: "Invalid password");
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> IsAuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<ErrorOr<bool>> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return Error.Failure(code: "IdentityError", description: "User not found");
        }

        await _userManager.DeleteAsync(user);
        return true;

    }

    public async Task<ErrorOr<bool>> DeleteUserAsync(AppUser user)
    {
        if (user == null)
        {
            return Error.Failure(code: "IdentityError", description: "User not found");
        }

        await _userManager.DeleteAsync(user);
        return true;


    }
}
