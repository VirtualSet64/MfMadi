using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;

namespace Infrastructure.Repository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<News> GetNews()
        {
            return Get().Where(x => x.IsDeleted != true);
        }
    }
}
