using DomainService.Dto;
using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repository.Interfaces
{
    public interface INewsRepository : IGenericRepository<News>
    {
        public IQueryable<News> GetNews();
        public List<NewsWithMainImage> GetNewsWithFirstImage();
    }
}
