namespace DomainService.Entity
{
    public class Menu
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public bool? TopMainPageIsVisible { get; set; }        
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public int? ParentId { get; set; }
    }
}
