namespace DomainService.Entity
{
    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value{ get; set; }
        public ContactType? ContactType { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
