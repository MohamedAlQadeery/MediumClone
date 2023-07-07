using FluentValidation;
using MediumClone.Api.Contracts.ProductCategory.Request;


namespace MediumClone.Api.Contracts.ProductCategory.Validator;

public class CreateProductCategoryValidator : AbstractValidator<CreateProductCategoryRequest>
{
    public CreateProductCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(200)
            .WithMessage("Description must not exceed 200 characters");

        // RuleFor(x => x.Image)
        // .Must(BeAValidImage)
        // .WithMessage("Image must be a valid image file")
        // .Must(HaveValidDimensions)
        // .WithMessage("Image dimensions must be between 100x100 and 1000x1000 pixels");


    }



    // private bool BeAValidImage(IFormFile? file)
    // {
    //     if (file == null)
    //     {
    //         return true; // Skip validation if no file is uploaded
    //     }

    //     if (file.ContentType.ToLower() != "image/jpeg" &&
    //         file.ContentType.ToLower() != "image/png" &&
    //         file.ContentType.ToLower() != "image/gif")
    //     {
    //         return false;
    //     }

    //     return true;
    // }

    // private bool HaveValidDimensions(IFormFile? file)
    // {
    //     if (file == null)
    //     {
    //         return true; // Skip validation if no file is uploaded
    //     }

    //     using var image = Image.Load(file.OpenReadStream());
    //     if (image.Width < 100 || image.Width > 1000 ||
    //         image.Height < 100 || image.Height > 1000)
    //     {
    //         return false;
    //     }

    //     return true;
    // }
}