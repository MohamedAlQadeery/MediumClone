using ErrorOr;
namespace MediumClone.Domain.Common.DomainErrors;
public static partial class Errors
{
    public static class User
    {

        public static Error DuplicateEmail =>
         Error.Validation(code: "DuplicateEmail", description: "Email already exists");


        public static Error UserIsNotFollowed =>
         Error.Validation(code: "UserIsNotFollowed", description: "User is not followed");

        public static Error UserIsAlreadyFollowed =>
            Error.Validation(code: "UserIsAlreadyFollowed", description: "User is already followed");
    }
}