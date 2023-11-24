using DomainService.Dto;
using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repository.Interfaces
{
    public interface INewsRepository : IGenericRepository<News>
    {
        public IQueryable<News> GetNews();
        public News GetNewsById(int newsId);
        public List<NewsWithMainImage> GetNewsWithFirstImage();
    }
}
