using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using MfMadi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileModelController : Controller
    {
        private readonly IFileModelRepository _fileModelRepository;
        private readonly IAddFileOnServer _addFileOnServer;

        public FileModelController(IFileModelRepository fileModelRepository, IAddFileOnServer addFileOnServer)
        {
            _fileModelRepository = fileModelRepository;
            _addFileOnServer = addFileOnServer;
        }

        [Route("CreateFileModel")]
        [HttpPost]
        public async Task<IActionResult> CreateFileModel(int contentId, IFormFileCollection formFiles)
        {
            if (formFiles != null)
            {
                List<FileModel> fileModels = new();
                foreach (var file in formFiles)
                {
                    await _addFileOnServer.CreateFile(file);
                    fileModels.Add(new FileModel()
                    {
                        ContentId = contentId,
                        Name = file.FileName
                    });
                }
                await _fileModelRepository.CreateRange(fileModels);
            }
            return Ok();
        }

        [Route("DeleteFileModel")]
        [HttpPost]
        public async Task<IActionResult> DeleteFileModel(int id)
        {
            var file = await _fileModelRepository.FindById(id);
            if (file == null)
                return BadRequest("Файл не найден");

            file.IsDeleted = true;
            file.UpdateDate = DateTime.Now;
            await _fileModelRepository.Update(file);
            return Ok();
        }
    }
}
