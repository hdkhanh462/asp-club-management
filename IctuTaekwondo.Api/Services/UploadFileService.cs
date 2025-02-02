using Microsoft.Extensions.Configuration;

namespace IctuTaekwondo.Api.Services
{
    public interface IUploadFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
        void DeleteFile(string fileName);
    }

    public class UploadFileService : IUploadFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public UploadFileService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public void DeleteFile(string fileName)
        {
            var contentPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads", fileName);

            if (!File.Exists(path)) throw new FileNotFoundException();

            File.Delete(path);
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var contentPath = _webHostEnvironment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var fileExtention = Path.GetExtension(file.FileName).ToLowerInvariant();

            var fileName = $"{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}_{Guid.NewGuid().ToString("N")}{fileExtention}";
            var fileNameWithPath = Path.Combine(path, fileName);

            using var steam = new FileStream(fileNameWithPath, FileMode.Create);
            await file.CopyToAsync(steam);

            return $"{_configuration["ApiUrl"]!}/static/{fileName}";
        }
    }
}
