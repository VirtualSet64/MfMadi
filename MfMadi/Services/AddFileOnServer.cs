using MfMadi.Services.Interfaces;

namespace MfMadi.Services
{
    public class AddFileOnServer : IAddFileOnServer
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private string FileFolderPath { get; set; }
        public AddFileOnServer(IWebHostEnvironment appEnvironment, IHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _appEnvironment = appEnvironment;
            FileFolderPath = hostEnvironment.ContentRootPath + configuration["FileFolder"];
        }

        public async Task<string> CreateFile(IFormFile uploadedFile)
        {
            string path = FileFolderPath + uploadedFile.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                await uploadedFile.CopyToAsync(fileStream);
            return path;
        }
    }
}
