using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobStorageWebApi.services.Storage;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly IConfiguration _configuration;

    public BlobService(BlobServiceClient blobServiceClient, IConfiguration configuration)
    {
        _blobServiceClient = blobServiceClient;
        _configuration = configuration;
       
    }

    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default)
    {
        var filesContainer = _configuration["AzureBlobStorageContainer:Files"];
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filesContainer);
        var fileId = Guid.NewGuid();
        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType },
            cancellationToken: cancellationToken);

        return fileId;
    }

    public async Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        var filesContainer = _configuration["AzureBlobStorageContainer:Files"];
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filesContainer);
        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        // Await the response from DownloadContentAsync
        var response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

        // Return the FileResponse with the stream and content type
        return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
    }


    public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        var filesContainer = _configuration["AzureBlobStorageContainer:Files"];
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filesContainer);
        BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<Guid> UploadJsonAsync(CustomerSettings settings, CancellationToken cancellationToken = default)
    {
        var filesContainer = _configuration["AzureBlobStorageContainer:Files"];
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filesContainer);
        var fileId = Guid.NewGuid();
        BlobClient blobClient = containerClient.GetBlobClient($"{fileId}.json");

        // Convert settings object to JSON string
        string jsonString = JsonSerializer.Serialize(settings);

        // Convert JSON string to stream
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "application/json" },
            cancellationToken: cancellationToken);

        return fileId;
    }
    
    public async Task<CustomerSettings> DownloadJsonAsync(Guid fileId, CancellationToken cancellationToken = default)
    {
        var filesContainer = _configuration["AzureBlobStorageContainer:Files"];
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filesContainer);
        BlobClient blobClient = containerClient.GetBlobClient($"{fileId}.json");

        // Await the response from DownloadContentAsync
        var response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

        // Convert JSON content to BrandingSettings object
        string jsonString = response.Value.Content.ToString();
        CustomerSettings settings = JsonSerializer.Deserialize<CustomerSettings>(jsonString);

        return settings;
    }
    
    public async Task UpdateJsonAsync(Guid fileId, CustomerSettings updatedSettings, CancellationToken cancellationToken = default)
    {
        var filesContainer = _configuration["AzureBlobStorageContainer:Files"];
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(filesContainer);
        BlobClient blobClient = containerClient.GetBlobClient($"{fileId}.json");

        // Convert updated settings object to JSON string
        string jsonString = JsonSerializer.Serialize(updatedSettings);

        // Convert JSON string to stream
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

        // Upload and overwrite the existing JSON file
        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = "application/json" },
            cancellationToken: cancellationToken);
    }



}