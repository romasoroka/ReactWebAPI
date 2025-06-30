using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebAPI.Data;
using ReactWebAPI.Dtos;
using ReactWebAPI.Models;

namespace ReactWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkSessionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public WorkSessionsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkSessionDto>>> GetSessions()
        {
            var entities = await _context.WorkSessions
                .Include(ws => ws.Project)
                .Include(ws => ws.Employee)
                .ToListAsync();
            var dtos = _mapper.Map<List<WorkSessionDto>>(entities);

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkSessionDto>> GetSession(int id)
        {
            var session = await _context.WorkSessions
                .Include(ws => ws.Project)
                .Include(ws => ws.Employee)
                .FirstOrDefaultAsync(ws => ws.Id == id);
            if (session == null) return NotFound();

            return Ok(_mapper.Map<WorkSessionDto>(session));
        }

        [HttpPost]
        public async Task<ActionResult<WorkSessionDto>> CreateSession([FromBody] WorkSessionDto dto)
        {
            if (dto.ProjectId <= 0 || dto.EmployeeId <= 0)
            {
                return BadRequest("ProjectId та EmployeeId повинні бути більше 0.");
            }

            var project = await _context.Projects.FindAsync(dto.ProjectId);
            if (project == null)
            {
                return BadRequest($"Проєкт з ID {dto.ProjectId} не існує.");
            }

            var employee = await _context.Employees.FindAsync(dto.EmployeeId);
            if (employee == null)
            {
                return BadRequest($"Працівник з ID {dto.EmployeeId} не існує.");
            }

            var entity = _mapper.Map<WorkSession>(dto);
            entity.Project = project;
            entity.Employee = employee;

            _context.WorkSessions.Add(entity);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<WorkSessionDto>(entity);
            return CreatedAtAction(nameof(GetSession), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, [FromBody] WorkSessionDto dto)
        {
            var session = await _context.WorkSessions
                .Include(ws => ws.Project)
                .Include(ws => ws.Employee)
                .FirstOrDefaultAsync(ws => ws.Id == id);
            if (session == null) return NotFound();

            if (dto.ProjectId <= 0 || dto.EmployeeId <= 0)
            {
                return BadRequest("ProjectId та EmployeeId повинні бути більше 0.");
            }

            var project = await _context.Projects.FindAsync(dto.ProjectId);
            if (project == null)
            {
                return BadRequest($"Проєкт з ID {dto.ProjectId} не існує.");
            }

            var employee = await _context.Employees.FindAsync(dto.EmployeeId);
            if (employee == null)
            {
                return BadRequest($"Працівник з ID {dto.EmployeeId} не існує.");
            }

            _mapper.Map(dto, session);
            session.Project = project;
            session.Employee = employee;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.WorkSessions.FindAsync(id);
            if (session == null) return NotFound();

            _context.WorkSessions.Remove(session);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}