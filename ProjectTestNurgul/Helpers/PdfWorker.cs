using Microsoft.Extensions.Configuration;
using ProjectTestNurgul.Services.Concrete;
using ProjectTestNurgul.Services.Model;
using PuppeteerSharp;
using System.Text;

namespace ProjectTestNurgul.Helpers
{
    public class PdfWorker : IFileWorker
    {
        private readonly IConfiguration _configuration;
        private readonly string _format;
        private readonly string _path;
        private readonly ILogger<PdfWorker> _logger;

        public PdfWorker(IConfiguration configuration, ILogger<PdfWorker> logger)
        {
            _configuration = configuration;
            var pathSection = _configuration.GetSection("CustomSettings:FilePath");
            _path = pathSection.Value != null ? pathSection.Value.ToString() : AppContext.BaseDirectory;
            _format = "pdf";
            _logger = logger;
        }

        public async Task<bool> Convert(IFormFile file, int fileId)
        {
            try
            {
                var result = new StringBuilder();
                var outputFilePath = GetPath(fileId);
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(reader.ReadLine());
                }
                var html = result.ToString();

                var browserFetcher = new BrowserFetcher();
                await browserFetcher.DownloadAsync();
                await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
                await using var page = await browser.NewPageAsync();
                await page.SetContentAsync(html);
                await page.PdfAsync(outputFilePath);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);
                return false;
            }
        }

        public void DeleteFileFromDirectory(int fileId)
        {
            var path = GetPath(fileId);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public byte[] GetBytesFromPath(string localFilePath)
        {
            return File.ReadAllBytes(localFilePath);
        }

        public MemoryStream GetMemoryStreamFromFile(int fileId)
        {
            var pdfPath = GetPath(fileId);
            byte[] bytes = GetBytesFromPath(pdfPath);
            var memoryStream = new MemoryStream(bytes);
            return memoryStream;
        }

        public string GetPath(int fileId)
        {
            return Path.Join(_path, $"{fileId}.{_format}");
        }
    }
}
