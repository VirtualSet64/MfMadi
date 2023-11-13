namespace DomainService.Entity
{
    public class Content
    {
        public int Id { get; set; }
        public string? MainText { get; set; }
        public string? Title { get; set; }
        public List<FileModel>? FilesName { get; set; }
        public ContentType? ContentType { get; set; }

        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
