using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Content> GetContents()
        {
            return Get().Include(x => x.ParentContent).Include(x => x.FileModels).Where(x => x.IsDeleted != true);
        }
    }
}
