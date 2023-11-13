﻿using DomainService.Dto.Account;
using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [Route("GetEmployees")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeRepository.Get().Include(x => x.Role));
        }

        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(RegistrationDto model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new()
                {
                    Name = model.Login,
                    Password = model.Password,
                    Role = model.Role,
                    RoleId = model.RoleId == null ? null : model.RoleId
                };

                await _employeeRepository.Create(employee);
                return Ok();
            }
            return BadRequest();
        }

        [Route("EditUser")]
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditDto model)
        {
            if (ModelState.IsValid)
            {
                Employee? employee = _employeeRepository.Get().FirstOrDefault(x => x.Id == model.Id);
                if (employee != null)
                {
                    employee.Name = model.Login;
                    employee.Password = model.Password;
                    employee.Role = model.Role;
                    employee.RoleId = model.RoleId == null ? null : model.RoleId;

                    await _employeeRepository.Update(employee);
                }
            }
            return BadRequest();
        }

        [Route("DeleteUser")]
        [HttpPost]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            Employee? employee = _employeeRepository.Get().FirstOrDefault(c => c.Id.ToString() == id);
            if (employee == null)
                return BadRequest("Пользователь не найден");

            await _employeeRepository.Remove(employee);
            return Ok();
        }
    }
}
