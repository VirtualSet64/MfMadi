using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;

namespace Infrastructure.Repository
{
    public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
    {
        public PartnerRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Partner> GetPartners()
        {
            return Get().Where(x => x.IsDeleted != true);
        }
    }
}
