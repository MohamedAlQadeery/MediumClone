

using ErrorOr;

using MediatR;
using MediumClone.Application.Authentication.Common;
using MediumClone.Application.Authentication.Queries.Login;
using MediumClone.Application.Common.Interfaces;
using MediumClone.Application.Common.Interfaces.Authentication;

namespace MediumClone.Application.Authentication.Commands.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IIdentityService _identityService;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IIdentityService identityService)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _identityService = identityService;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = await _identityService.LoginAsync(query.Email, query.Password);
        if (user.IsError)
        {
            return user.FirstError;
        }

        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user.Value);

        return new AuthenticationResult(
            user.Value,
            token);
    }
}