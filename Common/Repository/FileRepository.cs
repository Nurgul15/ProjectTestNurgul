using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Common.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly TestDataContext _dataContext;
        public FileRepository(TestDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task DeleteAsync(int Id)
        {
            var entity = await _dataContext.FileData.FirstOrDefaultAsync(x => x.Id == Id);
            if (entity == null) return;
            _dataContext.FileData.Remove(entity);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<FileData?> GetAsync(int Id)
        {
           return await _dataContext.FileData.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IList<FileData>> GetAllAsync()
        {
            return await _dataContext.FileData.ToListAsync();
        }

        public async Task<FileData> AddAsync(FileData entity)
        {
            _dataContext.FileData.Add(entity);

            await _dataContext.SaveChangesAsync();

            return entity;
        }

        public async Task<FileData> AddFileAsync(string fileName)
        {
            var fileData = new FileData() { Name = fileName };
            await AddAsync(fileData);
            return fileData;
        }
    }
}
