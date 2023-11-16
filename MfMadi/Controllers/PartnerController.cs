using DomainService.Entity;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartnerController : Controller
    {
        private readonly IPartnerRepository _partnerRepository;

        public PartnerController(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
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

        [Route("CreatePartner")]
        [HttpPost]
        public async Task<IActionResult> CreatePartner(Partner partner)
        {
            partner.CreateDate = DateTime.Now;
            await _partnerRepository.Create(partner);
            return Ok();
        }

        [Route("UpdatePartner")]
        [HttpPost]
        public async Task<IActionResult> UpdatePartner(Partner partner)
        {
            partner.CreateDate = DateTime.Now;
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
            await _partnerRepository.Update(partner);
            return Ok();
        }
    }
}
