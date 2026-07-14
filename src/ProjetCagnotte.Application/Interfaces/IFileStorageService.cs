using ProjetCagnotte.Application.DTOs;

namespace ProjetCagnotte.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveImageAsync(UploadedFileDto file);
}
