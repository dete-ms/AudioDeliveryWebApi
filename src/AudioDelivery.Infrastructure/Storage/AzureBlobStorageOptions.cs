namespace AudioDelivery.Infrastructure.Storage;

/// <summary>
/// Configuration options for Azure Blob Storage.
/// </summary>
public class AzureBlobStorageOptions
{
    public const string ConnectionStringName = "AzureBlobStorageConnectionString";

    public string ConnectionString { get; set; } = string.Empty;
}
