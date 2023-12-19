using DomainService.Dto.Enums;

namespace DomainService.Entity
{
    /// <summary>
    /// Контент
    /// </summary>
    public class Content
    {
        public int Id { get; set; }
        /// <summary>
        /// Заглавный текст
        /// </summary>
        public string? MainText { get; set; }
        /// <summary>
        /// Основной текст
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Html из ckeditor
        /// </summary>
        public string? HtmlContent { get; set; }
        /// <summary>
        /// Список файлов
        /// </summary>
        public List<FileModel>? FileModels { get; set; }        
        public ContentType? ContentType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// Id родительского объекта
        /// </summary>
        public int? ParentContentId { get; set; }
        public Content? ParentContent { get; set; }
        public int? NewsId { get; set; }
        public int? AdvertisingId { get; set; }
    }
}
