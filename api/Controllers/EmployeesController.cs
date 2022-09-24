using api.Controllers.Models;
using api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly ApiDbContext _myApiDbContext;

        public EmployeesController(ApiDbContext myApiDbContext)
        {
            _myApiDbContext = myApiDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _myApiDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _myApiDbContext.Employees.AddAsync(employeeRequest);
            await _myApiDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            {
                var employee = await _myApiDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }


        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _myApiDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _myApiDbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)

        {
            var employee = await _myApiDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _myApiDbContext.Employees.Remove(employee);
            await _myApiDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}
