using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using MfMadi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [Route("GenerateHtmlContent")]
        [HttpPost]
        public IActionResult GenerateHtmlContent(string path, string contentTitle, string contentHtml)
        {
            _generateHtmlContent.GenerateHtml(path, contentTitle, contentHtml);
            return Ok();
        }

        [Authorize]
        [Route("CreateContent")]
        [HttpPost]
        public async Task<IActionResult> CreateContent(Content content)
        {
            if (content.Link != null)
                _generateHtmlContent.GenerateHtml(content.Link, content.Title, content.HtmlContent);
            
            content.CreateDate = DateTime.Now;
            await _contentRepository.Create(content);
            return Ok(content);
        }

        [Authorize]
        [Route("UpdateContent")]
        [HttpPost]
        public async Task<IActionResult> UpdateContent(Content content)
        {
            if (content.Link != null)
            {
                var oldContent = await _contentRepository.FindById(content.Id);
                _generateHtmlContent.DeleteGeneratedHtmlContent(oldContent.Link);
                _generateHtmlContent.GenerateHtml(content.Link, content.Title, content.HtmlContent);
            }                

            content.UpdateDate = DateTime.Now;
            await _contentRepository.Update(content);
            return Ok();
        }

        [Authorize]
        [Route("DeleteContent")]
        [HttpPost]
        public async Task<IActionResult> DeleteContent(int id)
        {
            var content = await _contentRepository.FindById(id);
            if (content == null)
                return BadRequest("Контент не найден");
            if (content.Link != null)
                _generateHtmlContent.DeleteGeneratedHtmlContent(content.Link);

            content.IsDeleted = true;
            content.UpdateDate = DateTime.Now;
            await _contentRepository.Update(content);
            return Ok();
        }

        [Authorize]
        [Route("DeleteGeneratedHtmlContent")]
        [HttpPost]
        public IActionResult DeleteGeneratedHtmlContent(string path)
        {
            _generateHtmlContent.DeleteGeneratedHtmlContent(path);
            return Ok();
        }
    }
}
