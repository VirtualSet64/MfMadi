using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repository.Interfaces
{
    public interface IPartnerRepository : IGenericRepository<Partner>
    {
        public IQueryable<Partner> GetPartners();
    }
}
