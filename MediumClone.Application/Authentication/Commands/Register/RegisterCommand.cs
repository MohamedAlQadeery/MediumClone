using ErrorOr;
using MediatR;
using MediumClone.Application.Authentication.Common;
using MediumClone.Domain.Common.ValueObjects;


namespace MediumClone.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password, Address Address) : IRequest<ErrorOr<AuthenticationResult>>;