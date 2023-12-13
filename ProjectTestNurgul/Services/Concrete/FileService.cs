using Common.Repository;
using ProjectTestNurgul.Enums;
using ProjectTestNurgul.Helpers;
using ProjectTestNurgul.Services.Abstract;
using ProjectTestNurgul.Services.Model;

namespace ProjectTestNurgul.Services.Concrete
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileWorker _pdfWorker;
        private readonly ILogger<FileService> _logger;
        public FileService(IFileRepository fileRepository, ILogger<FileService> logger, IFileWorker pdfWorker)
        {            
            _fileRepository = fileRepository;
            _logger = logger;
            _pdfWorker = pdfWorker;           
        }

        public MemoryStream GetMemoryStreamFromFile(int fileId, FileFormat format)
        {
            switch(format)
            {
                case FileFormat.Pdf:
                    return _pdfWorker.GetMemoryStreamFromFile(fileId);
                default:
                    return _pdfWorker.GetMemoryStreamFromFile(fileId);
            }         
        }

        public async Task<bool> ConvertToFormat(IFormFile file, int fileId, FileFormat format)
        {
            switch (format)
            {
                case FileFormat.Pdf:
                    return await _pdfWorker.Convert(file, fileId);
                default:
                    return await _pdfWorker.Convert(file, fileId);
            }
        }

        public byte[] GetBytesFromPath(string localFilePath)
        {
            return File.ReadAllBytes(localFilePath);
        }

        public async Task<List<FileViewModel>> GetAllFileData()
        {
            var files = (await _fileRepository.GetAllAsync()).Select(x =>
                        new FileViewModel()
                        {
                            Id = x.Id,
                            Name = x.Name
                        }).ToList();
            return files;
        }

        public async Task<FileViewModel?> GetFileDataById(int Id)
        {
            var file = await _fileRepository.GetAsync(Id);

            if (file == null) return null;

            return new FileViewModel()
            {
                Id = file.Id,
                Name = file.Name
            };
        }

        public async Task DeleteFileById(int Id)
        {
            try
            {
                await _fileRepository.DeleteAsync(Id);
                _pdfWorker.DeleteFileFromDirectory(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task<int> AddFile(string fileName)
        {
          var addedFile =  await _fileRepository.AddFileAsync(fileName);
            return addedFile.Id;
        }

        public DownloadModel GetBytesAndPath(int fileId)
        {
            var path = _pdfWorker.GetPath(fileId);
            return new DownloadModel() { FileAsBytes = GetBytesFromPath(path), Path = path };
        }
    }
}
