using DomainService.Entity;
using Infrastructure.Repository.Interfaces;

namespace MfMadi.Common
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(string adminLogin, string password, IEmployeeRepository employeeRepository, IRoleRepository roleRepository)
        {
            if (roleRepository.Get().FirstOrDefault(x => x.Name == "admin") == null)
            {
                await roleRepository.Create(new Role("admin"));
            }
            if (employeeRepository.Get().FirstOrDefault(x => x.Name == adminLogin) == null)
            {
                Employee admin = new() { Name = adminLogin, Password = password, RoleId = roleRepository.Get().FirstOrDefault(x => x.Name == "admin")?.Id };
                await employeeRepository.Create(admin);
            }
        }
    }
}
