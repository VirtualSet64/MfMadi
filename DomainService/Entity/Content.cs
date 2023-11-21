namespace DomainService.Entity
{
    public class Content
    {
        public int Id { get; set; }
        public string? MainText { get; set; }
        public string? Title { get; set; }
        public string? HtmlContent { get; set; }
        public List<FileModel>? FileModels { get; set; }        
        public ContentType? ContentType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public int? ParentId { get; set; }
    }
}
