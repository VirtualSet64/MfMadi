namespace DomainService.Entity
{
    public class News
    {
        public int Id { get; set; }
        public string? MainText { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public Content? Content { get; set; }
    }
}
