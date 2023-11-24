using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public Advertising GetAdvertisingById(int advertisingId)
        {
            return Get().Include(x => x.Content).FirstOrDefault(x => x.Id == advertisingId);
        }
    }
}
