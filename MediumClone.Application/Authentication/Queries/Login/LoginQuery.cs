using ErrorOr;
using MediatR;
using MediumClone.Application.Authentication.Common;

namespace MediumClone.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;