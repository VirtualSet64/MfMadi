using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        [Route("GetAllMenu")]
        [HttpGet]
        public IActionResult GetAllMenu()
        {
            return Ok(_menuRepository.Get());
        }

        [Route("GetMenu")]
        [HttpGet]
        public IActionResult GetMenu()
        {
            return Ok(_menuRepository.GetMenu());
        }

        [Route("GetMenuById")]
        [HttpGet]
        public IActionResult GetMenuById(int menuId)
        {
            return Ok(_menuRepository.GetMenuById(menuId));
        }

        [Route("CreateMenu")]
        [HttpPost]
        public async Task<IActionResult> CreateMenu(Menu menu)
        {
            menu.CreateDate = DateTime.Now;
            await _menuRepository.Create(menu);
            return Ok();
        }

        [Route("UpdateMenu")]
        [HttpPost]
        public async Task<IActionResult> UpdateMenu(Menu menu)
        {
            menu.UpdateDate = DateTime.Now;
            await _menuRepository.Update(menu);
            return Ok();
        }

        [Route("DeleteMenu")]
        [HttpPost]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _menuRepository.FindById(id);
            if (menu == null)
                return BadRequest("Пункт меню не найден");
            menu.IsDeleted = true;
            menu.UpdateDate = DateTime.Now;
            await _menuRepository.Update(menu);
            return Ok();
        }
    }
}
