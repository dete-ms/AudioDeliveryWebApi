using AudioDelivery.Application.Common.Interfaces;

namespace AudioDelivery.Infrastructure.Storage;

public class AzureBlobStorageService : IStorageService
{
    public Task DeleteAsync(string fileUrl)
    {
        throw new NotImplementedException();
    }

    public Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        throw new NotImplementedException();
    }
}
