using DomainService.DbService;
using DomainService.Dto;
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
            var news = Get().Where(x => x.IsDeleted != true).Include(x => x.Content).ThenInclude(x => x.FileModels).OrderByDescending(x => x.CreateDate).AsQueryable();
            if (take != null)
                news = news.Take((int)take);
            if (skip != null)
                news = news.Skip((int)skip);
            return news;
        }

        public News GetNewsById(int newsId)
        {
            return Get().Include(x => x.Content).ThenInclude(x => x.FileModels).FirstOrDefault(x => x.Id == newsId);
        }
    }
}
