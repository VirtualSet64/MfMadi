﻿using DomainService.Dto.Account;
using DomainService.Entity;
using Infrastructure.Repository.Interfaces;
using MfMadi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MfMadi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly AuthOptions _authOptions;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountController(IEmployeeRepository employeeRepository, AuthOptions authOptions)
        {
            _employeeRepository = employeeRepository;
            _authOptions = authOptions;
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginDto loginData)
        {
            Employee? employee = _employeeRepository.Get().Include(x => x.Role).FirstOrDefault(p => p.Name == loginData.Login && p.Password == loginData.Password);
            
            if (employee != null)
            {
                EmployeeDto employeeDto = new()
                {
                    EmployeeId = employee.Id,
                    EmployeeName = employee.Name,
                    Role = employee.Role
                };
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, employee.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, employee.Role.Name)
                };
                ClaimsIdentity claimsIdentity = new(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                if (claimsIdentity == null)
                    return BadRequest(new { errorText = "Invalid username or password." });

                // создаем JWT-токен
                var jwt = _authOptions.GetAuthData(claimsIdentity.Claims);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    employeeDto = employeeDto
                };
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
