using AudioDelivery.Application.Common.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AudioDelivery.Infrastructure.Storage;

public class AzureBlobStorageService : IStorageService
{
    private readonly BlobServiceClient _serviceClient;

    public AzureBlobStorageService(IOptions<AzureBlobStorageOptions> options)
    {
        _serviceClient = new BlobServiceClient(options.Value.ConnectionString);
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, string containerName, CancellationToken cancellationToken = default)
    {
        var containerClient = _serviceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        var headers = new BlobHttpHeaders { ContentType = contentType };

        await blobClient.UploadAsync(fileStream, new BlobUploadOptions
        {
            HttpHeaders = headers
        }, cancellationToken);

        return blobClient.Uri.ToString();
    }

    public Task<string> UploadAsync(
        Stream fileStream,
        Guid fileId,
        string contentType,
        AzureContainerStorageType containerName,
        CancellationToken cancellationToken = default)
    {
        return this.UploadAsync(fileStream, fileId.ToString(), contentType, containerName.ToString().ToLowerInvariant(), cancellationToken);
    }

    public Task<string> UploadAsync(IFormFile formFile, AzureContainerStorageType containerName, CancellationToken cancellationToken = default)
    {
        return this.UploadAsync(
            formFile.OpenReadStream(), 
            Guid.NewGuid().ToString(), 
            formFile.ContentType, 
            containerName.ToString().ToLowerInvariant(), 
            cancellationToken);
    }

    public async Task DeleteAsync(string fileUrl, CancellationToken cancellationToken = default)
    {
        // Derive the container name and blob name from the full URL.
        // URL path structure: /<container-name>/<blob-name>
        var uri = new Uri(fileUrl);
        var segments = uri.AbsolutePath.TrimStart('/').Split('/', 2);
        var containerName = segments[0];
        var blobName = segments[1];

        var containerClient = _serviceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.DeleteIfExistsAsync(cancellationToken:cancellationToken);
    }
}
