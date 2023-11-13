using DomainService.Entity;
using System.ComponentModel.DataAnnotations;

namespace DomainService.Dto.Account
{
    public class RegistrationDto
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        [Display(Name = "Роль")]
        public Role? Role { get; set; }

        [Display(Name = "Id Роли")]
        public int? RoleId { get; set; }
    }
}
