namespace DomainService.Entity
{
    public class FileModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ContentId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
