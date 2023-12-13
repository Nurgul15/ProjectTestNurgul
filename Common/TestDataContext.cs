using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class TestDataContext : DbContext
    {
        public string ConnectionString { get; set; }

        public TestDataContext(DbContextOptions<TestDataContext> options) : base(options)
        {
        }

        public DbSet<FileData> FileData { get; set; }
    }
}
