using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public RoleController(IRoleRepository roleRepository, IEmployeeRepository employeeRepository)
        {
            _roleRepository = roleRepository;
            _employeeRepository = employeeRepository;
        }

        [Route("GetRoles")]
        [HttpGet]
        public IActionResult GetRoles()
        {
            return Ok(_roleRepository.Get().ToList());
        }

        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
                await _roleRepository.Create(new Role(name));
            return Ok();
        }

        [Route("EditRole")]
        [HttpPost]
        public async Task<IActionResult> EditRole(int employeeId, int roleId)
        {
            Employee? employee = await _employeeRepository.FindById(employeeId);
            if (employee != null)
            {
                employee.RoleId = roleId;
                await _employeeRepository.Update(employee);

                return Ok();
            }
            return BadRequest("Такой пользователь не найден");
        }

        [Route("DeleteRole")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(int id)
        {
            Role? role = await _roleRepository.FindById(id);
            if (role != null)
                await _roleRepository.Remove(role);
            return Ok();
        }
    }
}
