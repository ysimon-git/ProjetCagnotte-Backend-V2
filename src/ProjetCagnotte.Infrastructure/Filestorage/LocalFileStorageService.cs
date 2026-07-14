using Microsoft.AspNetCore.Hosting;
using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;

namespace ProjetCagnotte.Infrastructure.FileStorage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _environment;

    public LocalFileStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveImageAsync(UploadedFileDto file)
    {
        var extension =Path.GetExtension(file.FileName);

        var fileName =$"{Guid.NewGuid()}{extension}";

        var imagesFolder = Path.Combine(
            _environment.WebRootPath,
            "images",
            "products");

        Directory.CreateDirectory(imagesFolder);

        var filePath =Path.Combine(imagesFolder, fileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create);

        await file.Content.CopyToAsync(fileStream);

        return $"/images/products/{fileName}";
    }
}