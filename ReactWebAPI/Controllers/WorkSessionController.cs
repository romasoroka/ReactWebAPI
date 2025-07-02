using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactApplication.Application.Services;
using ReactApplication.Dtos;
using ReactApplication.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkSessionsController : ControllerBase
    {
        private readonly IWorkSessionService _workSessionService;
        private readonly IMapper _mapper;

        public WorkSessionsController(IWorkSessionService workSessionService, IMapper mapper)
        {
            _workSessionService = workSessionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkSessions()
        {
            try
            {
                var workSessions = await _workSessionService.GetAllAsync();
                return Ok(workSessions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні робочих сесій: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkSession(int id)
        {
            try
            {
                var workSession = await _workSessionService.GetByIdAsync(id);
                if (workSession == null)
                    return NotFound("Робочу сесію з таким ID не знайдено.");
                return Ok(workSession);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні робочої сесії: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkSession([FromBody] WorkSessionDto dto)
        {
            try
            {
                var workSession = await _workSessionService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetWorkSession), new { id = workSession.Id }, workSession);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при створенні робочої сесії: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkSession(int id, [FromBody] WorkSessionDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID робочої сесії в URL не відповідає ID у тілі запиту.");
                await _workSessionService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Робочу сесію з таким ID не знайдено.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при оновленні робочої сесії: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkSession(int id)
        {
            try
            {
                await _workSessionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Робочу сесію з таким ID не знайдено.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при видаленні робочої сесії: " + ex.Message);
            }
        }
    }
}