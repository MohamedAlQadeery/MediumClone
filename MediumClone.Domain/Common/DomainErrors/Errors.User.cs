using ErrorOr;
namespace MediumClone.Domain.Common.DomainErrors;
public static partial class Errors
{
    public static class User
    {

        public static Error DuplicateEmail =>
         Error.Validation(code: "DuplicateEmail", description: "Email already exists");
    }
}