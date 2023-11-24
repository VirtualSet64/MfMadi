using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainMenuController : Controller
    {
        private readonly IMainMenuRepository _mainMenuRepository;

        public MainMenuController(IMainMenuRepository mainMenuRepository)
        {
            _mainMenuRepository = mainMenuRepository;
        }

        [Route("GetAllMainMenu")]
        [HttpGet]
        public IActionResult GetAllMainMenu()
        {
            return Ok(_mainMenuRepository.Get());
        }

        [Route("GetMainMenu")]
        [HttpGet]
        public IActionResult GetMainMenu()
        {
            return Ok(_mainMenuRepository.GetMainMenu());
        }

        [Route("GetMainMenuById")]
        [HttpGet]
        public IActionResult GetMainMenuById(int mainMenuId)
        {
            return Ok(_mainMenuRepository.GetMainMenuById(mainMenuId));
        }

        [Route("CreateMainMenu")]
        [HttpPost]
        public async Task<IActionResult> CreateMainMenu(MainMenu mainMenu)
        {
            mainMenu.CreateDate = DateTime.Now;
            await _mainMenuRepository.Create(mainMenu);
            return Ok();
        }

        [Route("UpdateMainMenu")]
        [HttpPost]
        public async Task<IActionResult> UpdateMainMenu(MainMenu mainMenu)
        {
            mainMenu.UpdateDate = DateTime.Now;
            await _mainMenuRepository.Update(mainMenu);
            return Ok();
        }

        [Route("DeleteMainMenu")]
        [HttpPost]
        public async Task<IActionResult> DeleteMainMenu(int id)
        {
            var mainMenu = await _mainMenuRepository.FindById(id);
            if (mainMenu == null)
                return BadRequest("Пункт меню не найден");
            mainMenu.IsDeleted = true;
            mainMenu.UpdateDate = DateTime.Now;
            await _mainMenuRepository.Update(mainMenu);
            return Ok();
        }
    }
}
