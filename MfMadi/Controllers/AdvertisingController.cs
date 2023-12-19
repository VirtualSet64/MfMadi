using DomainService.Entity;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using MfMadi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdvertisingController : Controller
    {
        private readonly IAdvertisingRepository _advertisingRepository;
        private readonly IConfiguration _configuration;
        private readonly IAddFileOnServer _addFileOnServer;

        public AdvertisingController(IAdvertisingRepository advertisingRepository, IConfiguration configuration, IAddFileOnServer addFileOnServer)
        {
            _advertisingRepository = advertisingRepository;
            _addFileOnServer = addFileOnServer;
            _configuration = configuration;
        }

        /// <summary>
        /// Получение всех объявлений
        /// </summary>
        /// <returns></returns>
        [Route("GetAllAdvertisings")]
        [HttpGet]
        public IActionResult GetAllAdvertisings()
        {
            return Ok(_advertisingRepository.Get());
        }

        /// <summary>
        /// Получение неудаленных объявлений
        /// </summary>
        /// <returns></returns>
        [Route("GetAdvertisings")]
        [HttpGet]
        public IActionResult GetAdvertisings()
        {
            return Ok(_advertisingRepository.GetAdvertisings());
        }

        /// <summary>
        /// Получение объявления в нижней части главной страницы
        /// </summary>
        /// <returns></returns>
        [Route("GetMainPageDownAdvertising")]
        [HttpGet]
        public IActionResult GetMainPageDownAdvertising()
        {
            var advertising = _advertisingRepository.Get().FirstOrDefault(x => x.IsDeleted != true && x.MainPageDownIsVisible == true);
            if (advertising == null)
                return BadRequest("Не найдено объявление для нижней части главной страницы");
            return Ok(_advertisingRepository.Get().FirstOrDefault(x => x.IsDeleted != true && x.MainPageDownIsVisible == true));
        }

        /// <summary>
        /// Получение последних двух объявлений
        /// </summary>
        /// <returns></returns>
        [Route("GetLastAdvertisings")]
        [HttpGet]
        public IActionResult GetLastAdvertisings()
        {
            return Ok(_advertisingRepository.GetAdvertisings().OrderByDescending(x => x.CreateDate).Take(int.Parse(_configuration["CountOutputAdvertisings"])));
        }

        /// <summary>
        /// Получение объявления по Id
        /// </summary>
        /// <returns></returns>
        [Route("GetAdvertisingById")]
        [HttpGet]
        public IActionResult GetAdvertisingById(int advertisingId)
        {
            return Ok(_advertisingRepository.GetAdvertisingById(advertisingId));
        }

        /// <summary>
        /// Добавление файла к объявлению
        /// </summary>
        /// <param name="advertisingId"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [Route("AddFileToAdvertising")]
        [HttpPost]
        public async Task<IActionResult> AddFileToAdvertising(int advertisingId, IFormFile formFile)
        {
            var advertising = await _advertisingRepository.FindById(advertisingId);
            if (advertising == null)
                return BadRequest("Не найдено объявление");
            
            if (formFile != null)
            {
                await _addFileOnServer.CreateFile(formFile);
                advertising.AvatarFileName = formFile.FileName;
            }
            advertising.UpdateDate = DateTime.Now; 
            await _advertisingRepository.Update(advertising);
            return Ok();
        }

        [Route("CreateAdvertising")]
        [HttpPost]
        public async Task<IActionResult> CreateAdvertising(Advertising advertising)
        {
            advertising.CreateDate = DateTime.Now;
            await _advertisingRepository.Create(advertising);
            return Ok(advertising);
        }

        [Route("UpdateAdvertising")]
        [HttpPost]
        public async Task<IActionResult> UpdateAdvertising(Advertising advertising)
        {
            advertising.UpdateDate = DateTime.Now;
            await _advertisingRepository.Update(advertising);
            return Ok();
        }

        [Route("DeleteAdvertising")]
        [HttpPost]
        public async Task<IActionResult> DeleteAdvertising(int id)
        {
            var advertising = await _advertisingRepository.FindById(id);
            if (advertising == null)
                return BadRequest("Объявление не найдено");
            advertising.IsDeleted = true;
            advertising.UpdateDate = DateTime.Now;
            await _advertisingRepository.Update(advertising);
            return Ok();
        }
    }
}
