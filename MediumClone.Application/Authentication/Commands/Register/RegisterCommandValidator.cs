using FluentValidation;

namespace MediumClone.Application.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Address.City).NotEmpty();
        RuleFor(x => x.Address.Country).NotEmpty();
        RuleFor(x => x.Address.Street).NotEmpty();
        RuleFor(x => x.Address.ZipCode).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}