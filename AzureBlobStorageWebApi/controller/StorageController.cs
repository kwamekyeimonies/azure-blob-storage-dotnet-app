using AzureBlobStorageWebApi.services.Storage;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobStorageWebApi.controller;

[ApiController]
[Route("api/files")]
public class StorageController : ControllerBase
{
    private readonly IBlobService _blobService;

    public StorageController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    [HttpPost("upload")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> UploadFileAsync(IFormFile formFile)
    {
        using Stream stream = formFile.OpenReadStream();
        Guid fileId = await _blobService.UploadAsync(stream, formFile.ContentType);

        return Ok(fileId);
    }
    [HttpGet("file/{fileId}")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> GetFileAsync(Guid fileId)
    {
        try
        {
            FileResponse fileResponse = await _blobService.DownloadAsync(fileId);
        
            return File(fileResponse.Stream, fileResponse.ContentType);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = $"File with ID {fileId} not found.", error = ex.Message });
        }
    }
    
    [HttpDelete("file/{fileId}")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> DeleteFileAsync(Guid fileId)
    {
        try
        {
            await _blobService.DeleteAsync(fileId);
            return NoContent(); // Return 204 No Content if the deletion was successful
        }
        catch (Exception ex)
        {
            return NotFound(new { message = $"File with ID {fileId} not found.", error = ex.Message });
        }
    }
    
    [HttpPost("json/upload")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> UploadJsonAsync([FromBody] CustomerSettings settings)
    {
        Guid fileId = await _blobService.UploadJsonAsync(settings);
        return Ok(new { message = "JSON uploaded successfully.", fileId });
    }

    [HttpGet("json/{fileId}")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> GetJsonAsync(Guid fileId)
    {
        try
        {
            CustomerSettings settings = await _blobService.DownloadJsonAsync(fileId);
            return Ok(settings);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = $"JSON file with ID {fileId} not found.", error = ex.Message });
        }
    }

    [HttpPut("json/{fileId}")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> UpdateJsonAsync(Guid fileId, [FromBody] CustomerSettings updatedSettings)
    {
        try
        {
            await _blobService.UpdateJsonAsync(fileId, updatedSettings);
            return Ok(new { message = "JSON updated successfully." });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = $"JSON file with ID {fileId} not found.", error = ex.Message });
        }
    }


}