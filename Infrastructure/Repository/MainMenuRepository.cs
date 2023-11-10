using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;

namespace Infrastructure.Repository
{
    public class MainMenuRepository : GenericRepository<MainMenu>, IMainMenuRepository
    {
        public MainMenuRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
