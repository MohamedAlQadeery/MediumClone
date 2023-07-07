using ErrorOr;

namespace MediumClone.Domain.Common.DomainErrors;
public static partial class Errors
{
    public static class Salesman
    {

        public static Error InvalidSalesmanId =>
         Error.Validation(code: "InvalidSalesmanId", description: "Salesman Id is invalid");
    }



}