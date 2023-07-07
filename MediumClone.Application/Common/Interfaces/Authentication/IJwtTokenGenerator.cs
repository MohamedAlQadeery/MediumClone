

using MediumClone.Domain.AppUserEntity;

namespace MediumClone.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(AppUser user);
}