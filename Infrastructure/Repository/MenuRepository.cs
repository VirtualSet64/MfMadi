using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Menu> GetMenu()
        {
            return Get().Where(x => x.IsDeleted != true);
        }

        public Menu GetMenuById(int menuId)
        {
            return Get().Include(x => x.Content).ThenInclude(x => x.FileModels).FirstOrDefault(x => x.Id == menuId);
        }
    }
}
