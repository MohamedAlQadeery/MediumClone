using ErrorOr;

namespace MediumClone.Api.Abstractions;

public interface IImageService
{
    Task<ErrorOr<string>> UploadImageAsync(IFormFile imageFile);
    Task<ErrorOr<string>> UploadImageAsyncWithoutResize(IFormFile imageFile);
    Task<ErrorOr<string>> UploadSmallImageAsync(IFormFile imageFile);
    Task<ErrorOr<string>> UploadImageWithCustomSize(IFormFile imageFile, int width, int height);


    Task ResizeImageAsync(string filePath, string uploadedFolder, string fileName);

    bool ValidateImageDimension(IFormFile image, int width, int height);
}