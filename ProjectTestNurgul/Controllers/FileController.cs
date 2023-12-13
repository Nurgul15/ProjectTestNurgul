using Microsoft.AspNetCore.Mvc;
using ProjectTestNurgul.Enums;
using ProjectTestNurgul.Helpers;
using ProjectTestNurgul.Model;
using ProjectTestNurgul.Services.Abstract;

namespace ProjectTestNurgul.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("Open/{fileId}")]
        public IActionResult Open(int fileId)
        {
            try
            {
                var memoryStream = _fileService.GetMemoryStreamFromFile(fileId, FileFormat.Pdf);              
                return new FileStreamResult(memoryStream, "application/pdf");
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to open file");
            }
        }

        [HttpGet("Download/{fileId}")]
        public async Task<IActionResult> Download(int fileId)
        {
            var file = await _fileService.GetFileDataById(fileId);
            var result = _fileService.GetBytesAndPath(fileId);
            var fileName = $"{Path.GetFileNameWithoutExtension(file.Name)}.{EnumHelper.GetDescription(FileFormat.Pdf)}";
            return File(result.FileAsBytes, "application/pdf", fileName);
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadModel model)
        {
            var fileName = $"{Path.GetFileNameWithoutExtension(model.File.FileName)}.{EnumHelper.GetDescription(FileFormat.Pdf)}";
            var entityId = await _fileService.AddFile(fileName);
            var result = await _fileService.ConvertToFormat(model.File, entityId, FileFormat.Pdf);
            if (result)
                return Ok(result);
            else
                return BadRequest("Conversion error");
        }

        [HttpDelete("Delete/{fileId}")]
        public async Task<IActionResult> Delete(int fileId)
        {
            await _fileService.DeleteFileById(fileId);
            return Ok();
        }

        [HttpGet("GetFiles")]
        public async Task<IActionResult> GetFiles()
        {
            var result = await _fileService.GetAllFileData();
            return Ok(result);
        }
    }
}
