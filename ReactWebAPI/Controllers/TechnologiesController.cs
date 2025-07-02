using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactApplication.Application.Services;
using ReactApplication.Dtos;
using ReactApplication.Services;
using ReactDomain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechnologiesController : ControllerBase
    {
        private readonly ITechnologyService _technologyService;
        private readonly IMapper _mapper;

        public TechnologiesController(ITechnologyService technologyService, IMapper mapper)
        {
            _technologyService = technologyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTechnologies()
        {
            try
            {
                var technologies = await _technologyService.GetAllAsync();
                return Ok(technologies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні технологій: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTechnology(int id)
        {
            try
            {
                var technology = await _technologyService.GetByIdAsync(id);
                if (technology == null)
                    return NotFound("Технологію з таким ID не знайдено.");
                return Ok(technology);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні технології: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTechnology([FromBody] Technology dto)
        {
            try
            {
                var technology = await _technologyService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetTechnology), new { id = technology.Id }, technology);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при створенні технології: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTechnology(int id, [FromBody] Technology dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID технології в URL не відповідає ID у тілі запиту.");
                await _technologyService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Технологію з таким ID не знайдено.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при оновленні технології: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTechnology(int id)
        {
            try
            {
                await _technologyService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Технологію з таким ID не знайдено.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при видаленні технології: " + ex.Message);
            }
        }
    }
}