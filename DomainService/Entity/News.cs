namespace DomainService.Entity
{
    public class News
    {
        public int Id { get; set; }
        public string? MainText { get; set; }
        public string? Name { get; set; }
        public string? AvatarFileName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
