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
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            var employees = await _context.Employees
                .ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        [HttpPost("create")]
        public async Task<ActionResult<Project>> CreateProject([FromBody] Project project)
        {
            if (project == null)
                return BadRequest("Project cannot be null");

            project.Technologies ??= new List<string>();
            project.Programmers ??= new List<Employee>();
            project.Credentials ??= new List<Credential>();
            project.Analytics ??= new ProjectAnalytics { HoursLogged = 0, Reports = 0 };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetProjects), new { id = project.Id }, project);
        }

    }
}
