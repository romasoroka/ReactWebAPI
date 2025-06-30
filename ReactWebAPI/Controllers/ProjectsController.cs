using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebAPI.Data;
using ReactWebAPI.Dtos;
using ReactWebAPI.Models;

namespace ReactWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
        {
            var dtos = await _context.Projects
                .Include(p => p.Technologies)
                .Include(p => p.Credentials)
                .Include(p => p.Employees)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProject(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Technologies)
                .Include(p => p.Credentials)
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return NotFound();

            return Ok(_mapper.Map<ProjectDto>(project));
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] ProjectDto dto)
        {
            if (dto.EmployeeIds == null) dto.EmployeeIds = new List<int>();
            if (dto.Technologies == null) dto.Technologies = new List<string>();
            if (dto.Credentials == null) dto.Credentials = new List<CredentialDto>();

            if (dto.EmployeeIds.Any(id => id <= 0))
            {
                return BadRequest("EmployeeIds містить невалідне значення (наприклад, 0). Усі ідентифікатори повинні бути більше 0.");
            }

            var project = _mapper.Map<Project>(dto);

            if (dto.EmployeeIds.Any())
            {
                var employees = await _context.Employees
                    .Where(e => dto.EmployeeIds.Contains(e.Id))
                    .ToListAsync();
                if (employees.Count != dto.EmployeeIds.Count)
                {
                    return BadRequest("Один або кілька EmployeeIds не існують.");
                }
                project.Employees = employees;
            }

            if (dto.Technologies.Any())
            {
                var technologies = new List<Technology>();
                foreach (var techName in dto.Technologies)
                {
                    var tech = await _context.Technologies
                        .FirstOrDefaultAsync(t => t.Name == techName);
                    if (tech == null)
                    {
                        tech = new Technology { Name = techName };
                        _context.Technologies.Add(tech);
                    }
                    technologies.Add(tech);
                }
                project.Technologies = technologies;
            }

            if (dto.Credentials.Any())
            {
                var credentials = new List<Credential>();
                foreach (var credDto in dto.Credentials)
                {
                    var credential = await _context.Credentials
                        .FirstOrDefaultAsync(c => c.Name == credDto.Name && c.Value == credDto.Value);
                    if (credential == null)
                    {
                        credential = _mapper.Map<Credential>(credDto);
                        _context.Credentials.Add(credential);
                    }
                    credentials.Add(credential);
                }
                project.Credentials = credentials;
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<ProjectDto>(project);
            return CreatedAtAction(nameof(GetProject), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDto dto)
        {
            var project = await _context.Projects
                .Include(p => p.Technologies)
                .Include(p => p.Credentials)
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return NotFound();

            _mapper.Map(dto, project);

            project.Employees.Clear();
            project.Technologies.Clear();
            project.Credentials.Clear();

            if (dto.EmployeeIds != null && dto.EmployeeIds.Any())
            {
                if (dto.EmployeeIds.Any(id => id <= 0))
                {
                    return BadRequest("EmployeeIds містить невалідне значення (наприклад, 0). Усі ідентифікатори повинні бути більше 0.");
                }

                var employees = await _context.Employees
                    .Where(e => dto.EmployeeIds.Contains(e.Id))
                    .ToListAsync();
                if (employees.Count != dto.EmployeeIds.Count)
                {
                    return BadRequest("Один або кілька EmployeeIds не існують.");
                }
                project.Employees = employees;
            }

            if (dto.Technologies != null && dto.Technologies.Any())
            {
                var technologies = new List<Technology>();
                foreach (var techName in dto.Technologies)
                {
                    var tech = await _context.Technologies
                        .FirstOrDefaultAsync(t => t.Name == techName);
                    if (tech == null)
                    {
                        tech = new Technology { Name = techName };
                        _context.Technologies.Add(tech);
                    }
                    technologies.Add(tech);
                }
                project.Technologies = technologies;
            }

            if (dto.Credentials != null && dto.Credentials.Any())
            {
                var credentials = new List<Credential>();
                foreach (var credDto in dto.Credentials)
                {
                    var credential = await _context.Credentials
                        .FirstOrDefaultAsync(c => c.Name == credDto.Name && c.Value == credDto.Value);
                    if (credential == null)
                    {
                        credential = _mapper.Map<Credential>(credDto);
                        _context.Credentials.Add(credential);
                    }
                    credentials.Add(credential);
                }
                project.Credentials = credentials;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}