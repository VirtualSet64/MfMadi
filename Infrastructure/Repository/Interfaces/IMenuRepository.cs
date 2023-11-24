using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repository.Interfaces
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        public IQueryable<Menu> GetMenu();
        public Menu GetMenuById(int menuId);
    }
}
