using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Entity
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
        public Role? Role { get; set; }
        public int? RoleId { get; set; }
    }
}
