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
    public class CredentialsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CredentialsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CredentialDto>>> GetCredentials()
        {
            var credentials = await _context.Credentials
                .ToListAsync();
            var dtos = _mapper.Map<List<CredentialDto>>(credentials);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CredentialDto>> GetCredential(int id)
        {
            var credential = await _context.Credentials.FindAsync(id);
            if (credential == null) return NotFound();

            return Ok(_mapper.Map<CredentialDto>(credential));
        }

        [HttpPost]
        public async Task<ActionResult<CredentialDto>> CreateCredential([FromBody] CredentialDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Назва облікових даних не може бути порожньою.");
            }

            var existingCredential = await _context.Credentials
                .FirstOrDefaultAsync(c => c.Name == dto.Name && c.Value == dto.Value);
            if (existingCredential != null)
            {
                return Conflict($"Облікові дані з назвою '{dto.Name}' і значенням '{dto.Value}' уже існують.");
            }

            var credential = _mapper.Map<Credential>(dto);
            _context.Credentials.Add(credential);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<CredentialDto>(credential);
            return CreatedAtAction(nameof(GetCredential), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCredential(int id, [FromBody] CredentialDto dto)
        {
            var credential = await _context.Credentials.FindAsync(id);
            if (credential == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest("Назва облікових даних не може бути порожньою.");
            }

            var existingCredential = await _context.Credentials
                .FirstOrDefaultAsync(c => c.Name == dto.Name && c.Value == dto.Value && c.Id != id);
            if (existingCredential != null)
            {
                return Conflict($"Облікові дані з назвою '{dto.Name}' і значенням '{dto.Value}' уже існують.");
            }

            _mapper.Map(dto, credential);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCredential(int id)
        {
            var credential = await _context.Credentials.FindAsync(id);
            if (credential == null) return NotFound();

            _context.Credentials.Remove(credential);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}