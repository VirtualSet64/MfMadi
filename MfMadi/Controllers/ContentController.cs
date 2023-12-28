using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;

        public ContentController(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        [Route("GetAllContents")]
        [HttpGet]
        public IActionResult GetAllContents()
        {
            return Ok(_contentRepository.Get().Include(x => x.ParentContent));
        }

        [Route("GetContents")]
        [HttpGet]
        public IActionResult GetContents()
        {
            return Ok(_contentRepository.GetContents());
        }

        [Route("GetContentById")]
        [HttpGet]
        public IActionResult GetContentById(int contentId)
        {
            return Ok(_contentRepository.GetContents().FirstOrDefault(x=>x.Id == contentId));
        }

        [Route("CreateContent")]
        [HttpPost]
        public async Task<IActionResult> CreateContent(Content content)
        {
            content.CreateDate = DateTime.Now;
            await _contentRepository.Create(content);
            return Ok();
        }

        [Route("UpdateContent")]
        [HttpPost]
        public async Task<IActionResult> UpdateContent(Content content)
        {
            content.UpdateDate = DateTime.Now;
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
            content.UpdateDate = DateTime.Now;
            await _contentRepository.Update(content);
            return Ok();
        }
    }
}
