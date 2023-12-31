﻿
using ErrorOr;
using MediumClone.Domain.AppUserEntity;

namespace MediumClone.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> IsAuthorizeAsync(string userId, string policyName);

    Task<ErrorOr<AppUser>> CreateUserAsync(AppUser user, string password);


    Task<ErrorOr<bool>> DeleteUserAsync(string userId);


    Task<ErrorOr<AppUser>> LoginAsync(string email, string password);
}
