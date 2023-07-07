using ErrorOr;
using MediumClone.Api.Abstractions;
using SixLabors.ImageSharp;

namespace MediumClone.Api.Common.Services;

public class ImageService : IImageService
{

    private const string uploadDir = "./wwwroot/images";

    public async Task<ErrorOr<string>> UploadImageAsync(IFormFile imageFile)
    {
        if (!imageFile.ContentType.StartsWith("image/"))
        {
            return Errors.CommonErrors.Images.InvalidImageFormat;
        }

        // ContentType = "image/jpeg"
        // Output: "jpeg"
        var fileExtension = imageFile.ContentType.Split('/')[1];
        var fileName = Guid.NewGuid().ToString();
        fileName = $"{fileName}.{fileExtension}";
        await StoreUploadedImage(imageFile, fileName);


        return fileName;

    }

    private async Task StoreUploadedImage(IFormFile imageFile, string fileName)
    {
        if (!Directory.Exists(uploadDir))
        {
            Directory.CreateDirectory(uploadDir);
        }

        var filePath = Path.Combine(uploadDir, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        await ResizeImageAsync(filePath, uploadDir, fileName);


    }



    public async Task ResizeImageAsync(string filePath, string uploadedFolder, string fileName)
    {
        var folderSmall = Path.Combine(uploadedFolder, "thumbs", "small", fileName);
        var folderMedi = Path.Combine(uploadedFolder, "thumbs", "med", fileName);
        var folderBig = Path.Combine(uploadedFolder, "thumbs", "big", fileName);


        //images/jpg
        using Image input = Image.Load(filePath);
        input.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Stretch, Size = new Size(457, 666) }));

        await input.SaveAsync(folderBig);

        input.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Stretch, Size = new Size(518, 388) }));
        await input.SaveAsync(folderMedi);

        input.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Stretch, Size = new Size(340, 220) }));
        await input.SaveAsync(folderSmall);
    }

    public async Task<ErrorOr<string>> UploadImageAsyncWithoutResize(IFormFile imageFile)
    {
        if (!imageFile.ContentType.StartsWith("image/"))
        {
            return Errors.CommonErrors.Images.InvalidImageFormat;
        }

        // ContentType = "image/jpeg"
        // Output: "jpeg"
        var fileExtension = imageFile.ContentType.Split('/')[1];
        var fileName = Guid.NewGuid().ToString();
        fileName = $"{fileName}.{fileExtension}";

        if (!Directory.Exists(uploadDir))
        {
            Directory.CreateDirectory(uploadDir);
        }

        var filePath = Path.Combine(uploadDir, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }


        return fileName;
    }

    public async Task<ErrorOr<string>> UploadSmallImageAsync(IFormFile imageFile)
    {

        if (!imageFile.ContentType.StartsWith("image/"))
        {
            return Errors.CommonErrors.Images.InvalidImageFormat;
        }

        // ContentType = "image/jpeg"
        // Output: "jpeg"
        var fileExtension = imageFile.ContentType.Split('/')[1];
        var fileName = Guid.NewGuid().ToString();
        fileName = $"{fileName}.{fileExtension}";

        if (!Directory.Exists(uploadDir))
        {
            Directory.CreateDirectory(uploadDir);
        }

        var filePath = Path.Combine(uploadDir, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }


        var folderSmall = Path.Combine(uploadDir, "thumbs", "small", fileName);

        using (Image input = Image.Load(filePath))
        {
            //input.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Crop, Size = new Size(199, 131) }));
            input.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(199, 131),
                Mode = ResizeMode.Stretch
            }));
            await input.SaveAsync(folderSmall);

        }


        return fileName;
    }

    public async Task<ErrorOr<string>> UploadImageWithCustomSize(IFormFile imageFile, int width, int height)
    {

        if (!imageFile.ContentType.StartsWith("image/"))
        {
            return Errors.CommonErrors.Images.InvalidImageFormat;
        }

        if (!ValidateImageDimension(imageFile, width, height))
        {
            return Errors.CommonErrors.Images.InvalidDimension;
        }

        // ContentType = "image/jpeg"
        // Output: "jpeg"
        var fileExtension = imageFile.ContentType.Split('/')[1];
        var fileName = Guid.NewGuid().ToString();
        fileName = $"{fileName}.{fileExtension}";

        if (!Directory.Exists(uploadDir))
        {
            Directory.CreateDirectory(uploadDir);
        }

        var filePath = Path.Combine(uploadDir, fileName);
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }


        var customFolder = Path.Combine(uploadDir, "custom", fileName);

        using (Image input = Image.Load(filePath))
        {
            input.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(width, height),
                Mode = ResizeMode.Stretch
            }));
            await input.SaveAsync(customFolder);

        }


        return fileName;
    }

    public bool ValidateImageDimension(IFormFile image, int width, int height)
    {

        using var stream = image.OpenReadStream();
        using var imageSharp = Image.Load(stream);
        if (imageSharp.Width < width || imageSharp.Height < height)
        {

            return false;
        }

        return true;

    }
}



