using Microsoft.AspNetCore.Mvc;
using Gemma4Test.Models;
using System.Text.Json;

namespace Gemma4Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly string _uploadPath;

        public UploadController(IWebHostEnvironment env)
        {
            _uploadPath = Path.Combine(env.ContentRootPath, "Files");
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadChunk(IFormFile data, [FromForm] string fileName, [FromForm] int chunkNumber, [FromForm] int totalChunks, [FromForm] long offset)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                return BadRequest("Invalid multipart data.");
            }

            var path = Path.Combine(_uploadPath, fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            try
            {
                using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite,
                                               bufferSize: 81960,
                                               options: FileOptions.WriteThrough))
                {
                    fs.Position = offset;
                    await data.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("files")]
        [ResponseCache(Duration = 0, NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult GetFiles()
        {
            var files = Directory.GetFiles(_uploadPath)
                .Select(f => new UploadedFile
                {
                    Name = Path.GetFileName(f),
                    Size = new System.IO.FileInfo(f).Length,
                    Modified = new System.IO.FileInfo(f).LastWriteTime
                })
                .OrderByDescending(f => f.Modified)
                .ToList();

            return Ok(files);
        }

        [HttpGet("download")]
        public IActionResult DownloadFile([FromQuery] string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("File name is required.");

            var path = Path.Combine(_uploadPath, fileName);
            if (!System.IO.File.Exists(path))
                return NotFound("File not found.");

            var fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, "application/octet-stream", fileName);
        }

        [HttpPost("rename")]
        public IActionResult RenameFile([FromForm] string oldName, [FromForm] string newName)
        {
            if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(newName))
                return BadRequest("Both old and new names are required.");

            var oldPath = Path.Combine(_uploadPath, oldName);
            var newPath = Path.Combine(_uploadPath, newName);

            if (!System.IO.File.Exists(oldPath))
                return NotFound("Source file not found.");

            if (System.IO.File.Exists(newPath))
                return Conflict("A file with the new name already exists.");

            try
            {
                System.IO.File.Move(oldPath, newPath);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to rename file: {ex.Message}");
            }
        }

        [HttpDelete("delete")]
        public IActionResult DeleteFile([FromQuery] string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return BadRequest("File name is required.");

            var path = Path.Combine(_uploadPath, fileName);
            if (!System.IO.File.Exists(path))
                return NotFound("File not found.");

            try
            {
                System.IO.File.Delete(path);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete file: {ex.Message}");
            }
        }
    }
}
