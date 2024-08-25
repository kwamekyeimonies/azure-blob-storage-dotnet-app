namespace AzureBlobStorageWebApi.services.Storage;

public interface IBlobService
{
    Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);
    Task<FileResponse> DownloadAsync(Guid fileId, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid fileId, CancellationToken cancellationToken = default);
    public Task<Guid> UploadJsonAsync(CustomerSettings settings, CancellationToken cancellationToken = default);
    public  Task<CustomerSettings> DownloadJsonAsync(Guid fileId, CancellationToken cancellationToken = default);
    public Task UpdateJsonAsync(Guid fileId, CustomerSettings updatedSettings,
        CancellationToken cancellationToken = default);
}

