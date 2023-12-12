namespace DomainService.Entity
{
    public class Menu
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }       
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public Content? Content { get; set; }
        public int? MainMenuId { get; set; }
    }
}
