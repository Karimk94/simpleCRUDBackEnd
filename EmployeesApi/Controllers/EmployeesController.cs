using EmployeesApi.Data;
using EmployeesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeesApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly DBContext _dbContext;
        public EmployeesController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employeesList = await _dbContext.Employees.ToListAsync();

            return Ok(employeesList);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updatedEmployee)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            employee.Name = updatedEmployee.Name;
            employee.Email = updatedEmployee.Email;
            employee.Salary = updatedEmployee.Salary;
            employee.Phone = updatedEmployee.Phone;
            employee.Department = updatedEmployee.Department;

            await _dbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
