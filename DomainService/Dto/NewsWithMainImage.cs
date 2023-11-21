using DomainService.Entity;

namespace DomainService.Dto
{
    public class NewsWithMainImage
    {
        public News? News { get; set; }
        public string? Image { get; set; }
    }
}
