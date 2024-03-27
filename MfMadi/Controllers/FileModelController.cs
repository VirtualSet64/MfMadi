using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using MfMadi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FileModelController : Controller
    {
        private readonly IFileModelRepository _fileModelRepository;
        private readonly IInteractionFileOnServer _interactionFileOnServer;

        public FileModelController(IFileModelRepository fileModelRepository, IInteractionFileOnServer interactionFileOnServer)
        {
            _fileModelRepository = fileModelRepository;
            _interactionFileOnServer = interactionFileOnServer;
        }

        [Route("GetDeletedFileModels")]
        [HttpGet]
        public IActionResult GetDeletedFileModels()
        {
            var fileModels = _fileModelRepository.Get().Where(x => x.IsDeleted == true);
            return Ok(fileModels);
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
                    await _interactionFileOnServer.CreateFile(file);
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

        [Route("DeleteFileOnServer")]
        [HttpPost]
        public async Task<IActionResult> DeleteFileOnServer(int id)
        {
            var file = _fileModelRepository.Get().FirstOrDefault(x => x.Id == id);
            if (file == null)
                return BadRequest("Файл не найден");

            file.IsDeleted = true;
            file.UpdateDate = DateTime.Now;            
            _interactionFileOnServer.DeleteFile(file.Name);
            await _fileModelRepository.Remove(file);
            return Ok();
        }
    }
}
