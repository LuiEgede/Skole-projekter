using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientSideServer.Pages;

public class FilesModel : PageModel
{
    private readonly IWebHostEnvironment _environment;

    public FilesModel(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public List<FileInfoModel> Files { get; set; } = new();

    public void OnGet()
    {
        LoadFiles();
    }

    private void LoadFiles()
    {
        var uploadFolder = Path.Combine(_environment.ContentRootPath, "UploadedFiles");

        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        Files = Directory
            .GetFiles(uploadFolder)
            .Select(filePath => new FileInfoModel
            {
                FileName = Path.GetFileName(filePath),
                SizeInBytes = new FileInfo(filePath).Length,
                LastModified = System.IO.File.GetLastWriteTime(filePath)
            })
            .ToList();
    }

    public class FileInfoModel
    {
        public string FileName { get; set; } = string.Empty;
        public long SizeInBytes { get; set; }
        public DateTime LastModified { get; set; }
    }
}