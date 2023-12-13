using ProjectTestNurgul.Enums;
using ProjectTestNurgul.Services.Model;

namespace ProjectTestNurgul.Services.Abstract
{
    public interface IFileService
    {
        public MemoryStream GetMemoryStreamFromFile(int fileId, FileFormat format);

        Task<bool> ConvertToFormat(IFormFile file, int entityId, FileFormat format);

        byte[] GetBytesFromPath(string localFilePath);

        Task<List<FileViewModel>> GetAllFileData();

        Task<FileViewModel?> GetFileDataById(int id);

        Task DeleteFileById(int Id);

        Task<int> AddFile(string fileName);

        DownloadModel GetBytesAndPath(int fileId);
    }
}
