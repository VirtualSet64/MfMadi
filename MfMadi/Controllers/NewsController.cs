using DomainService.Entity;
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
        public IActionResult GetNews(int? skip = null, int? take = null)
        {
            return Ok(_newsRepository.GetNews(skip, take));
        }

        [Route("GetNewsById")]
        [HttpGet]
        public IActionResult GetNewsById(int newsId)
        {
            return Ok(_newsRepository.GetNewsById(newsId));
        }

        [Route("GetNewsWithFirstImage")]
        [HttpGet]
        public IActionResult GetNewsWithFirstImage()
        {
            return Ok(_newsRepository.GetNewsWithFirstImage());
        }

        [Route("CreateNews")]
        [HttpPost]
        public async Task<IActionResult> CreateNews(News news)
        {
            news.CreateDate = DateTime.Now;
            await _newsRepository.Create(news);
            return Ok(news);
        }

        [Route("UpdateNews")]
        [HttpPost]
        public async Task<IActionResult> UpdateNews(News news)
        {
            news.UpdateDate = DateTime.Now;
            await _newsRepository.Update(news);
            return Ok(news);
        }

        [Route("DeleteNews")]
        [HttpPost]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _newsRepository.FindById(id);
            if (news == null)
                return BadRequest("Новость не найдена");
            news.IsDeleted = true;
            news.UpdateDate = DateTime.Now;
            await _newsRepository.Update(news);
            return Ok();
        }
    }
}
