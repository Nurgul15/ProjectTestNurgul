namespace ProjectTestNurgul.Helpers
{
    public interface IFileWorker
    {
        Task<bool> Convert(IFormFile file, int fileId);

        byte[] GetBytesFromPath(string localFilePath);

        MemoryStream GetMemoryStreamFromFile(int fileId);

        void DeleteFileFromDirectory(int fileId);

        string GetPath(int fileId);
    }
}
