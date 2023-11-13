using DomainService.Entity;

namespace DomainService.Dto.Account
{
    public class EmployeeDto
    {
        public int? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public Role? Role { get; set; }
    }
}
