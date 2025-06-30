using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWebAPI.Data;
using ReactWebAPI.Models;

namespace ReactWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnologiesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TechnologiesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Technology>>> GetTechnologies()
        {
            var technologies = await _context.Technologies
                .Select(t => new Technology { Id = t.Id, Name = t.Name })
                .ToListAsync();
            return Ok(technologies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetTechnology(int id)
        {
            var technology = await _context.Technologies.FindAsync(id);
            if (technology == null) return NotFound();
            return Ok(technology.Name);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateTechnology([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Назва технології не може бути порожньою.");
            }

            var existingTech = await _context.Technologies
                .FirstOrDefaultAsync(t => t.Name == name);
            if (existingTech != null)
            {
                return Conflict($"Технологія з назвою '{name}' уже існує.");
            }

            var technology = new Technology { Name = name };
            _context.Technologies.Add(technology);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTechnology), new { id = technology.Id }, technology.Name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTechnology(int id, [FromBody] string name)
        {
            var technology = await _context.Technologies.FindAsync(id);
            if (technology == null) return NotFound();

            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Назва технології не може бути порожньою.");
            }

            var existingTech = await _context.Technologies
                .FirstOrDefaultAsync(t => t.Name == name && t.Id != id);
            if (existingTech != null)
            {
                return Conflict($"Технологія з назвою '{name}' уже існує.");
            }

            technology.Name = name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnology(int id)
        {
            var technology = await _context.Technologies.FindAsync(id);
            if (technology == null) return NotFound();

            _context.Technologies.Remove(technology);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}