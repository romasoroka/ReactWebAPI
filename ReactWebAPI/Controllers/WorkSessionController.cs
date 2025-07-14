using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using React.Application.Dtos;
using React.Application.Services.IServices;

namespace ReactWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkSessionsController : ControllerBase
{
    private readonly IWorkSessionService _workSessionService;

    public WorkSessionsController(IWorkSessionService workSessionService)
    {
        _workSessionService = workSessionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkSessions(CancellationToken cancellationToken)
    {
        var workSessions = await _workSessionService.GetAllAsync(cancellationToken);
        return Ok(workSessions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWorkSession(int id, CancellationToken cancellationToken)
    {
        var workSession = await _workSessionService.GetByIdAsync(id, cancellationToken);
        if (workSession == null)
            return NotFound("Робочу сесію з таким ID не знайдено.");
        return Ok(workSession);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkSession([FromBody] WorkSessionDto dto, CancellationToken cancellationToken)
    {
        var workSession = await _workSessionService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetWorkSession), new { id = workSession.Id }, workSession);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkSession(int id, [FromBody] WorkSessionDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
            return BadRequest("ID робочої сесії в URL не відповідає ID у тілі запиту.");
        await _workSessionService.UpdateAsync(id, dto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkSession(int id, CancellationToken cancellationToken)
    {
        await _workSessionService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}