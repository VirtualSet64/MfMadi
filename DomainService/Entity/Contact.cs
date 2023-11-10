namespace DomainService.Entity
{
    public class Contact
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Title{ get; set; }
        public ContactType? ContactType { get; set; }
    }
}
