using DomainService.Entity;
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

        [Route("GetAllAdvertisings")]
        [HttpGet]
        public IActionResult GetAllAdvertisings()
        {
            return Ok(_advertisingRepository.Get());
        }

        [Route("GetAdvertisings")]
        [HttpGet]
        public IActionResult GetAdvertisings()
        {
            return Ok(_advertisingRepository.GetAdvertisings());
        }

        [Route("GetLastAdvertisings")]
        [HttpGet]
        public IActionResult GetLastAdvertisings()
        {
            return Ok(_advertisingRepository.GetAdvertisings().OrderByDescending(x => x.CreateDate).Take(int.Parse(_configuration["CountOutputAdvertisings"])));
        }

        [Route("CreateAdvertising")]
        [HttpPost]
        public async Task<IActionResult> CreateAdvertising(Advertising advertising, IFormFile? formFile)
        {
            advertising.CreateDate = DateTime.Now;

            if (formFile != null)
            {
                await _addFileOnServer.CreateFile(formFile);
                advertising.AvatarFileName = formFile.FileName;
            }

            await _advertisingRepository.Create(advertising);
            return Ok();
        }

        [Route("UpdateAdvertising")]
        [HttpPost]
        public async Task<IActionResult> UpdateAdvertising(Advertising advertising, IFormFile? formFile)
        {
            advertising.UpdateDate = DateTime.Now;

            if (formFile != null)
            {
                await _addFileOnServer.CreateFile(formFile);
                advertising.AvatarFileName = formFile.FileName;
            }

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
