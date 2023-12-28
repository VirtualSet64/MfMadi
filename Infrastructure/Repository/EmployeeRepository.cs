using DomainService.DbService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Employee> GetEmployees()
        {
            return Get().Include(x => x.Role);
        }
    }
}
