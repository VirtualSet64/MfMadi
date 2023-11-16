using DomainService.Entity;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        [Route("GetAllNews")]
        [HttpGet]
        public IActionResult GetAllNews()
        {
            return Ok(_newsRepository.Get());
        }

        [Route("GetNews")]
        [HttpGet]
        public IActionResult GetNews()
        {
            return Ok(_newsRepository.GetNews());
        }

        [Route("CreateNews")]
        [HttpPost]
        public async Task<IActionResult> CreateNews(News news)
        {
            news.CreateDate = DateTime.Now;
            await _newsRepository.Create(news);
            return Ok();
        }

        [Route("UpdateNews")]
        [HttpPost]
        public async Task<IActionResult> UpdateNews(News news)
        {
            news.CreateDate = DateTime.Now;
            await _newsRepository.Update(news);
            return Ok();
        }

        [Route("DeleteNews")]
        [HttpPost]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _newsRepository.FindById(id);
            if (news == null)
                return BadRequest("Новость не найдена");
            news.IsDeleted = true;
            await _newsRepository.Update(news);
            return Ok();
        }
    }
}
