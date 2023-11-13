using DomainService.Entity;

namespace DomainService.Dto.Account
{
    public class EditDto
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public Role? Role { get; set; }
        public int? RoleId { get; set; }
    }
}
