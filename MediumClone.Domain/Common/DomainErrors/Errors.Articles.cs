using ErrorOr;

namespace MediumClone.Domain.Common.DomainErrors;
public static partial class Errors
{
    public static class Articles
    {

        public static Error TagsCantBeAssinged =>
         Error.Validation(code: "TagsCantBeAssinged", description: "Tags can't be assigned to article");



    }



}