using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;

namespace Infrastructure.Repository
{
    public class AdvertisingRepository : GenericRepository<Advertising>, IAdvertisingRepository
    {
        public AdvertisingRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Advertising> GetAdvertisings()
        {
            return Get().Where(x => x.IsDeleted != true);
        }
    }
}
