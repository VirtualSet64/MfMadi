using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using MfMadi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartnerController : Controller
    {
        private readonly IPartnerRepository _partnerRepository;
        private readonly IAddFileOnServer _addFileOnServer;

        public PartnerController(IPartnerRepository partnerRepository, IAddFileOnServer addFileOnServer)
        {
            _partnerRepository = partnerRepository;
            _addFileOnServer = addFileOnServer;
        }

        [Route("GetAllPartners")]
        [HttpGet]
        public IActionResult GetAllPartners()
        {
            return Ok(_partnerRepository.Get());
        }

        [Route("GetPartners")]
        [HttpGet]
        public IActionResult GetPartners()
        {
            return Ok(_partnerRepository.GetPartners());
        }

        [Route("GetPartnerById")]
        [HttpGet]
        public IActionResult GetPartnerById(int partnerId)
        {
            return Ok(_partnerRepository.GetPartners().FirstOrDefault(x=>x.Id == partnerId));
        }

        /// <summary>
        /// Создание партнера
        /// </summary>
        /// <param name="name"></param>
        /// <param name="link"></param>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [Route("CreatePartner")]
        [HttpPost]
        public async Task<IActionResult> CreatePartner(string name, string link, IFormFile? formFile)
        {
            var partner = new Partner()
            {
                Name = name,
                Link = link,
            };
            if (formFile != null)
            {
                await _addFileOnServer.CreateFile(formFile);
                partner.ImageFileName = formFile.FileName;
            }
            await _partnerRepository.Create(partner);
            return Ok();
        }

        [Route("UpdatePartner")]
        [HttpPost]
        public async Task<IActionResult> UpdatePartner(int partnerId, string name, string link, IFormFile? formFile)
        {
            var partner = await _partnerRepository.FindById(partnerId);
            partner.Name = name;
            partner.Link = link;
            if (formFile != null)
            {
                await _addFileOnServer.CreateFile(formFile);
                partner.ImageFileName = formFile.FileName;
            }
            await _partnerRepository.Update(partner);
            return Ok();
        }

        [Route("DeletePartner")]
        [HttpPost]
        public async Task<IActionResult> DeletePartner(int id)
        {
            var partner = await _partnerRepository.FindById(id);
            if (partner == null)
                return BadRequest("Ссылка на партнера не найдена");
            partner.IsDeleted = true;
            partner.UpdateDate = DateTime.Now;
            await _partnerRepository.Update(partner);
            return Ok();
        }
    }
}
