using ErrorOr;
using MediumClone.Api.Abstractions;

namespace MediumClone.Api.EndpointDefinitions;

public class UploadEndpointDefinition : BaseEndpointDefinition, IEndpointDefintion
{
    public void RegisterEndpoints(WebApplication app)
    {
        var files = app.MapGroup("/files");

        files.MapPost("/upload", UploadImage);

        files.MapPost("/upload-custom/{width}/{height}", UploadCustomImage);

        // Demo upload several files
        files.MapPost("/uploadMany", UploadManyImages);


    }

    #region Implementation of endpoints
    private async Task<IResult> UploadImage(HttpContext context, IImageService imageService, IFormFile file)
    {
        var result = await imageService.UploadImageAsync(file);
        return result.Match(
            success => Results.Ok(success),
            errors => ResultsProblem(context, errors));
    }

    private async Task<IResult> UploadCustomImage(HttpContext context, IImageService imageService, IFormFile file, int width, int height)
    {
        var result = await imageService.UploadImageWithCustomSize(file, width, height);

        return result.Match(
            success => Results.Ok(success),
            errors => ResultsProblem(context, errors));
    }

    private async Task<IResult> UploadManyImages(HttpContext context, IImageService imageService, IFormFileCollection files)
    {
        var filenames = new List<string>();
        foreach (var file in files)
        {
            var result = await imageService.UploadImageAsync(file);
            if (result.IsError)
            {
                return ResultsProblem(context, result.Errors);
            }
            filenames.Add(result.Value);
        }
        return TypedResults.Ok(filenames);
    }

    #endregion

}
