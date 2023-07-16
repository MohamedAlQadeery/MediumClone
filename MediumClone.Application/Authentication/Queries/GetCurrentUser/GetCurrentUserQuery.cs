using ErrorOr;
using MediatR;
using MediumClone.Application.Authentication.Common;
using Microsoft.AspNetCore.Http;

namespace MediumClone.Application.Authentication.Queries.GetCurrentUser;
public record GetCurrentUserQuery(HttpContext HttpContext) : IRequest<ErrorOr<AuthenticationResult>>;