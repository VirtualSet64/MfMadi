using DomainService.Entity;

namespace DomainService.Dto.Account
{
    public class RolesDto
    {
        public int EmployeeId { get; set; }
        public Role? Role { get; set; }
    }
}
