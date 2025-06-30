using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebAPI.Data;
using ReactWebAPI.Dtos;
using ReactWebAPI.Models;
using Shared.Events;

namespace ReactWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITopicProducer<EmployeeAdded> _producer;

        public EmployeesController(AppDbContext context, IMapper mapper, ITopicProducer<EmployeeAdded> producer)
        {
            _context = context;
            _mapper = mapper;
            _producer = producer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var entities = await _context.Employees
                .Include(e => e.Projects)
                .Include(e => e.WorkSessions)
                .ToListAsync();
            var dtos = _mapper.Map<List<EmployeeDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            var emp = await _context.Employees
                .Include(e => e.Projects)
                .Include(e => e.WorkSessions)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) return NotFound();

            return Ok(_mapper.Map<EmployeeDto>(emp));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto dto)
        {
            Console.WriteLine("Updating employee");
            if (id != dto.Id) return BadRequest("Employee ID mismatch");
            var emp = await _context.Employees
                .Include(e => e.Projects)
                .Include(e => e.WorkSessions)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) return NotFound();

            _mapper.Map(dto, emp);

            emp.Projects.Clear();
            if (dto.ProjectIds != null && dto.ProjectIds.Any())
            {
                var projects = await _context.Projects
                    .Where(p => dto.ProjectIds.Contains(p.Id))
                    .ToListAsync();
                if (projects.Count != dto.ProjectIds.Count)
                    return BadRequest("Some Project Ids are incorrect.");
                emp.Projects = projects;
            }

            emp.WorkSessions.Clear();
            if (dto.WorkSessions != null && dto.WorkSessions.Any())
            {
                var workSessions = _mapper.Map<List<WorkSession>>(dto.WorkSessions);
                foreach (var ws in workSessions)
                {
                    ws.EmployeeId = id;
                    var existingSession = await _context.WorkSessions.FindAsync(ws.Id);
                    if (existingSession == null)
                    {
                        _context.WorkSessions.Add(ws);
                    }
                    else
                    {
                        _context.Entry(existingSession).CurrentValues.SetValues(ws);
                    }
                    emp.WorkSessions.Add(ws);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeDto dto)
        {
            if (dto.ProjectIds == null)
            {
                dto.ProjectIds = new List<int>();
            }
            if (dto.ProjectIds.Any(id => id <= 0))
            {
                return BadRequest("ProjectIds has invalid value.");
            }
            var employee = _mapper.Map<Employee>(dto);

            if (dto.ProjectIds.Any())
            {
                var projects = await _context.Projects
                    .Where(p => dto.ProjectIds.Contains(p.Id))
                    .ToListAsync();

                if (projects.Count != dto.ProjectIds.Count)
                {
                    return BadRequest("Some Project Ids are incorrect.");
                }

                employee.Projects = projects;
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<EmployeeDto>(employee);
            await _producer.Produce(new EmployeeAdded(employee.Email));
            return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
        }

       

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null) return NotFound();

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}