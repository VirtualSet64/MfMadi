using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<News> GetNews(int? skip = null, int? take = null)
        {
            var news = Get().Include(x => x.Content).ThenInclude(x => x.FileModels.Where(c => c.IsDeleted != true))
                            .Where(x => x.IsDeleted != true && (x.Content == null || (x.Content != null && x.Content.IsDeleted != true)))
                            .OrderByDescending(x => x.CreateDate).AsQueryable();            
            if (skip != null)
                news = news.Skip((int)skip);
            if (take != null)
                news = news.Take((int)take);
            return news;
        }

        public News GetNewsById(int newsId)
        {
            return Get().Include(x => x.Content).ThenInclude(x => x.FileModels).FirstOrDefault(x => x.Id == newsId);
        }
    }
}
