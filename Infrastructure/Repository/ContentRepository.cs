using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;

namespace Infrastructure.Repository
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Content> GetContents()
        {
            return Get().Where(x => x.IsDeleted != true);
        }
    }
}
