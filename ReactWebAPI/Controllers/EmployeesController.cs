using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebAPI.Data;
using ReactWebAPI.Models;
using Shared.Events;

namespace ReactWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITopicProducer<EmployeeAdded> _producer;

        public EmployeesController(AppDbContext context, ITopicProducer<EmployeeAdded> producer)
        {
            _context = context;
            _producer = producer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.RecentWorkSessions)
                .ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest("Employee cannot be null");

            employee.Stats ??= new EmployeeStats();
            employee.Skills ??= new List<string>();
            employee.Projects ??= new List<string>();
            employee.RecentWorkSessions ??= new List<WorkSession>();

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            await _producer.Produce(new EmployeeAdded(employee.Email)); 

            return CreatedAtAction(nameof(GetEmployees), new { id = employee.Id }, employee);
        }
    }
}
