using DomainService.Entity;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using MfMadi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;
        private readonly IAddFileOnServer _addFileOnServer;
        private readonly IFileModelRepository _fileModelRepository;

        public ContentController(IContentRepository contentRepository, IAddFileOnServer addFileOnServer, IFileModelRepository fileModelRepository)
        {
            _contentRepository = contentRepository;
            _addFileOnServer = addFileOnServer;
            _fileModelRepository = fileModelRepository;
        }

        [Route("GetAllContents")]
        [HttpGet]
        public IActionResult GetAllContents()
        {
            return Ok(_contentRepository.Get());
        }

        [Route("GetContents")]
        [HttpGet]
        public IActionResult GetContents()
        {
            return Ok(_contentRepository.GetContents());
        }

        [Route("CreateContent")]
        [HttpPost]
        public async Task<IActionResult> CreateContent(Content content, IFormFileCollection? formFiles)
        {
            content.CreateDate = DateTime.Now;

            if (formFiles != null)
            {
                List<FileModel> fileModels = new();
                foreach (var file in formFiles)
                {
                    await _addFileOnServer.CreateFile(file);
                    fileModels.Add(new FileModel()
                    {
                        Name = file.FileName
                    });

                }
                content.FileModels = fileModels;
            }
            await _contentRepository.Create(content);
            return Ok();
        }

        [Route("UpdateContent")]
        [HttpPost]
        public async Task<IActionResult> UpdateContent(Content content, IFormFileCollection? formFiles)
        {
            content.CreateDate = DateTime.Now;

            if (formFiles != null)
            {
                List<FileModel> fileModels = new();
                foreach (var file in formFiles)
                {
                    await _addFileOnServer.CreateFile(file);
                    fileModels.Add(new FileModel()
                    {
                        Name = file.FileName
                    });

                }
                content.FileModels = fileModels;
            }
            await _contentRepository.Update(content);
            return Ok();
        }

        [Route("DeleteContent")]
        [HttpPost]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var content = await _contentRepository.FindById(id);
            if (content == null)
                return BadRequest("Контент не найден");
            content.IsDeleted = true;
            await _contentRepository.Update(content);
            return Ok();
        }

        [Route("DeleteFile")]
        [HttpPost]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _fileModelRepository.FindById(id);
            if (file == null)
                return BadRequest("Файл не найден");
            
            file.IsDeleted = true;
            return Ok();
        }
    }
}
