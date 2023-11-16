namespace DomainService.Entity
{
    public class MainMenu
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public bool? TopMainPageIsVisible { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;

        public List<Menu>? ChildMenu { get; set; }
    }
}
