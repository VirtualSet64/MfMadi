using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repository.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        public IQueryable<Contact> GetContacts();
    }
}
