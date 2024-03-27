using MfMadi.Services.Interfaces;

namespace MfMadi.Services
{
    public class InteractionFileOnServer : IInteractionFileOnServer
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private string FileFolderPath { get; set; }
        public InteractionFileOnServer(IWebHostEnvironment appEnvironment, IConfiguration configuration)
        {
            _appEnvironment = appEnvironment;
            FileFolderPath = appEnvironment.ContentRootPath + configuration["FileFolder"];
        }

        public async Task<string> CreateFile(IFormFile uploadedFile)
        {
            string path = FileFolderPath + uploadedFile.FileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
                await uploadedFile.CopyToAsync(fileStream);
            return path;
        }

        public void DeleteFile(string fileName)
        {
            string path = FileFolderPath + fileName;
            File.Delete(path);
        }
    }
}
