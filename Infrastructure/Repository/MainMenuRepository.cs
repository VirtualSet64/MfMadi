using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MainMenuRepository : GenericRepository<MainMenu>, IMainMenuRepository
    {
        public MainMenuRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<MainMenu> GetMainMenu()
        {
            return Get().Include(x=>x.ChildMenu).Where(x => x.IsDeleted != true);
        }

        public MainMenu GetMainMenuById(int mainMenuId)
        {
            return Get().Include(x => x.ChildMenu).FirstOrDefault(x => x.Id == mainMenuId);
        }
    }
}
