using Microsoft.AspNetCore.Mvc;

namespace ClientSideServer.Controllers;

// This API controller is responsible for managing files on the server.
[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly string _uploadFolderPath;

    public FilesController(IWebHostEnvironment environment)
    {
        _uploadFolderPath = Path.Combine(environment.ContentRootPath, "UploadedFiles");

        if (!Directory.Exists(_uploadFolderPath))
        {
            Directory.CreateDirectory(_uploadFolderPath);
        }
    }

    // Returns a list of all files stored in the upload folder.
    [HttpGet]
    public IActionResult GetFiles()
    {
        var files = Directory
            .GetFiles(_uploadFolderPath)
            .Select(filePath => new
            {
                FileName = Path.GetFileName(filePath),
                SizeInBytes = new FileInfo(filePath).Length,
                LastModified = System.IO.File.GetLastWriteTime(filePath)
            })
            .ToList();

        return Ok(files);
    }

    // Uploads a file to the server's upload folder.
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file was uploaded.");
        }

        var filePath = Path.Combine(_uploadFolderPath, Path.GetFileName(file.FileName));

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Ok(new
        {
            Message = "File uploaded successfully.",
            FileName = file.FileName
        });
    }
}
