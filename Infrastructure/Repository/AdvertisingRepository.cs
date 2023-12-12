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
            return Get().Include(x => x.Content).Where(x => x.IsDeleted != true && x.Content != null & x.Content.IsDeleted != true).OrderByDescending(x => x.CreateDate);
        }

        public Advertising GetAdvertisingById(int advertisingId)
        {
            return GetAdvertisings().FirstOrDefault(x => x.Id == advertisingId);
        }
    }
}
