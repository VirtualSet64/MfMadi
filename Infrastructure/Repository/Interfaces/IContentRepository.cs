using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repository.Interfaces
{
    public interface IContentRepository : IGenericRepository<Content>
    {
        public IQueryable<Content> GetContents();
    }
}
