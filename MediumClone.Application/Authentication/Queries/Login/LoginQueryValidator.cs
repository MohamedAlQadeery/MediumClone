using FluentValidation;
using MediumClone.Application.Authentication.Queries.Login;

namespace MediumClone.Application.Authentication.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}