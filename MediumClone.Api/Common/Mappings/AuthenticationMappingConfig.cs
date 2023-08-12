

using Mapster;
using MediumClone.Api.Contracts.Followings;
using MediumClone.Application.Authentication.Commands.Register;
using MediumClone.Application.Authentication.Common;
using MediumClone.Application.Authentication.Queries.Login;
using MediumClone.Contracts.Authentication;
using MediumClone.Domain.AppUserEntity.Enums;
using MediumClone.Domain.Common.ValueObjects;

namespace MediumClone.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AddressRequest, Address>()
    .ConstructUsing(src => Address.Create(src.Street, src.City, src.State, src.ZipCode));

        config.NewConfig<RegisterRequest, RegisterCommand>()
           .Map(dest => dest.FirstName, src => src.FirstName)
           .Map(dest => dest.LastName, src => src.LastName)
           .Map(dest => dest.Email, src => src.Email)
           .Map(dest => dest.Password, src => src.Password)
           .Map(dest => dest.Address, src => src.Address);

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
             .Map(dest => dest.Role, src => AppUserRole.FromValue(src.User.AppUserRole).Name)
             .Map(dest => dest.FollowingsIds, src => src.User.Followings.Select(x => x.FollowedUserId).ToList())
             .Map(dest => dest.FollowersIds, src => src.User.Followers.Select(x => x.FollowingUserId).ToList())

            .Map(dest => dest, src => src.User);
    }
}