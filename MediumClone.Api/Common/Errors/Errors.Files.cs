using ErrorOr;
namespace MediumClone.Api.Common.Errors;

public static partial class CommonErrors
{
    public static class Images
    {
        public static Error InvalidImageFormat =>
            Error.Validation(code: "InvalidImageFormat", description: "Image format is not supported.");
        public static Error InvalidDimension =>
            Error.Validation(code: "InvalidDimension", description: "Image dimensions are smaller than the requested size.");
    }
}