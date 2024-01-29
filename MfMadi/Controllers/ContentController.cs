using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using MfMadi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : Controller
    {
        private readonly IContentRepository _contentRepository;
        private readonly IGenerateHtmlContent _generateHtmlContent;

        public ContentController(IContentRepository contentRepository, IGenerateHtmlContent generateHtmlContent)
        {
            _contentRepository = contentRepository;
            _generateHtmlContent = generateHtmlContent;
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

        [Route("GetContentById")]
        [HttpGet]
        public IActionResult GetContentById(int contentId)
        {
            return Ok(_contentRepository.GetContents().FirstOrDefault(x => x.Id == contentId));
        }

        [Route("GenerateHtmlContent")]
        [HttpPost]
        public IActionResult GenerateHtmlContent(string path, string contentTitle, string contentHtml)
        {
            _generateHtmlContent.GenerateHtml(path, contentTitle, contentHtml);
            return Ok();
        }

        [Route("CreateContent")]
        [HttpPost]
        public async Task<IActionResult> CreateContent(Content content)
        {
            content.CreateDate = DateTime.Now;
            await _contentRepository.Create(content);
            return Ok(content);
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

        [Route("DeleteGeneratedHtmlContent")]
        [HttpPost]
        public IActionResult DeleteGeneratedHtmlContent(string path)
        {
            _generateHtmlContent.DeleteGeneratedHtmlContent(path);
            return Ok();
        }
    }
}
