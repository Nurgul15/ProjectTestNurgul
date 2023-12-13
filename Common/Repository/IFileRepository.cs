using Common.Models;

namespace Common.Repository
{
    public interface IFileRepository
    {
        Task<IList<FileData>> GetAllAsync();

        Task<FileData?> GetAsync(int Id);

        Task DeleteAsync(int Id);

        Task<FileData> AddAsync(FileData entity);

        Task<FileData> AddFileAsync(string fileName);
    }
}
