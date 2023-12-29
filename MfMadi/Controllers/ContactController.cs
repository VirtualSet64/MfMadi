using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [Route("GetAllContacts")]
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            return Ok(_contactRepository.Get());
        }

        [Route("GetContacts")]
        [HttpGet]
        public IActionResult GetContacts()
        {
            return Ok(_contactRepository.GetContacts());
        }

        [Route("GetContactsForLineOnMainPage")]
        [HttpGet]
        public IActionResult GetContactsForLineOnMainPage()
        {
            return Ok(_contactRepository.GetContacts().Where(x => x.IsTopMainPageVisible == true));
        }

        [Route("GetContactById")]
        [HttpGet]
        public IActionResult GetContactById(int contactId)
        {
            return Ok(_contactRepository.GetContacts().FirstOrDefault(x => x.Id == contactId));
        }

        [Route("CreateContact")]
        [HttpPost]
        public async Task<IActionResult> CreateContact(Contact contact)
        {
            contact.CreateDate = DateTime.Now;
            await _contactRepository.Create(contact);
            return Ok();
        }

        [Route("UpdateContact")]
        [HttpPost]
        public async Task<IActionResult> UpdateContact(Contact contact)
        {
            contact.UpdateDate = DateTime.Now;
            await _contactRepository.Update(contact);
            return Ok();
        }

        [Route("DeleteContact")]
        [HttpPost]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _contactRepository.FindById(id);
            if (contact == null)
                return BadRequest("Контакт не найден");
            contact.IsDeleted = true;
            contact.UpdateDate = DateTime.Now;
            await _contactRepository.Update(contact);
            return Ok();
        }
    }
}
