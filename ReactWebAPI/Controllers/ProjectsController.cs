using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactApplication.Application.Services;
using ReactApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                var projects = await _projectService.GetAllAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні проєктів: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            try
            {
                var project = await _projectService.GetByIdAsync(id);
                if (project == null)
                    return NotFound("Проєкт з таким ID не знайдено.");
                return Ok(project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні проєкту: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDto dto)
        {
            try
            {
                var project = await _projectService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при створенні проєкту: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID проєкту в URL не відповідає ID у тілі запиту.");
                await _projectService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Проєкт з таким ID не знайдено.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при оновленні проєкту: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                await _projectService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Проєкт з таким ID не знайдено.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при видаленні проєкту: " + ex.Message);
            }
        }
    }
}