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

        public IQueryable<News> GetNews()
        {
            return Get().Where(x => x.IsDeleted != true).Include(x => x.Content.FileModels);
        }

        public List<NewsWithMainImage> GetNewsWithFirstImage()
        {
            var news = Get().Where(x => x.IsDeleted != true).Include(x => x.Content.FileModels).ToList();

            List<NewsWithMainImage> newsWithMainImage = new();
            foreach (var item in news)
            {
                newsWithMainImage.Add(new NewsWithMainImage()
                {
                    News = item,
                    Image = item.Content?.FileModels?.First().Name
                });
            }
            return newsWithMainImage;
        }
    }
}
