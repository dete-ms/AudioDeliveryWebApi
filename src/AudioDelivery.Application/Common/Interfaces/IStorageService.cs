namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Defines methods for uploading and deleting files in a storage system asynchronously.
/// </summary>
/// <remarks>Implementations of this interface provide mechanisms to store and remove files, such as in cloud or
/// local storage.</remarks>
public interface IStorageService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteAsync(string fileUrl);
}
