namespace DomainService.Entity
{
    public class Advertising
    {
        public int Id { get; set; }
        public string? MainText { get; set; }
        public string? Title { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonLink { get; set; }
        public string? BoldTextButtom { get; set; }
        public string? AvatarFileName { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        public Content? Content { get; set; }
    }
}
