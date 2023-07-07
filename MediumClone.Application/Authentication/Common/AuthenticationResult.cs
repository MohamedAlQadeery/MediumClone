

using MediumClone.Domain.AppUserEntity;

namespace MediumClone.Application.Authentication.Common;

public record AuthenticationResult(
    AppUser User,
    string Token);