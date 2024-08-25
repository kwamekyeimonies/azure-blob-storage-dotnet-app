namespace AzureBlobStorageWebApi.services.Storage;


public class FileResponse : IDisposable
{
    public Stream Stream { get; }
    public string ContentType { get; }

    public FileResponse(Stream stream, string contentType)
    {
        Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
    }

    // Dispose method to clean up resources
    public void Dispose()
    {
        Stream?.Dispose();
    }
}
