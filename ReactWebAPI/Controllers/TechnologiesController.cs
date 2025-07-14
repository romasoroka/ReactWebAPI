using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using React.Application.Services.IServices;
using React.Domain.Entities;

namespace ReactWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TechnologiesController : ControllerBase
{
    private readonly ITechnologyService _technologyService;

    public TechnologiesController(ITechnologyService technologyService)
    {
        _technologyService = technologyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTechnologies(CancellationToken cancellationToken)
    {
        var technologies = await _technologyService.GetAllAsync(cancellationToken);
        return Ok(technologies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTechnology(int id, CancellationToken cancellationToken)
    {
        var technology = await _technologyService.GetByIdAsync(id, cancellationToken);
        if (technology == null)
            return NotFound("Технологію з таким ID не знайдено.");
        return Ok(technology);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTechnology([FromBody] Technology dto, CancellationToken cancellationToken)
    {
        var technology = await _technologyService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetTechnology), new { id = technology.Id }, technology);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTechnology(int id, [FromBody] Technology dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
            return BadRequest("ID технології в URL не відповідає ID у тілі запиту.");
        await _technologyService.UpdateAsync(id, dto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTechnology(int id, CancellationToken cancellationToken)
    {
        await _technologyService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}