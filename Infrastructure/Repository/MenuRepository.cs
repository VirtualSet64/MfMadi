using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;

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
    }
}
