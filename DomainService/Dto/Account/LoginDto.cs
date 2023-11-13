using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DomainService.Dto.Account
{
    public class LoginDto
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;
    }
}
