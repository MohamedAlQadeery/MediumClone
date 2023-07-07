
using ErrorOr;
using MediatR;
using MediumClone.Application.Authentication.Common;
using MediumClone.Application.Common.Interfaces;
using MediumClone.Application.Common.Interfaces.Authentication;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.AppUserEntity.Enums;

namespace MediumClone.Application.Authentication.Commands.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IIdentityService identityService)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _identityService = identityService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {

        //map command to app user
        var appUser = new AppUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Address = command.Address,
            AppUserRole = AppUserRole.User,
            UserName = command.Email

        };

        // 2. Create user (generate unique ID) & Persist to DB
        var result = await _identityService.CreateUserAsync(appUser, command.Password);

        if (result.IsError)
        {
            return result.FirstError;
        }


        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(result.Value);

        return new AuthenticationResult(
            result.Value,
            token);
    }
}