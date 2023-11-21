namespace DomainService.Entity
{
    public class MainMenu
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Link { get; set; }
        public bool? TopMainPageIsVisible { get; set; }
        public bool? SideMenuIsVisible { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public List<Menu>? ChildMenu { get; set; }
    }
}
