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
    public class CredentialsController : ControllerBase
    {
        private readonly ICredentialService _credentialService;
        private readonly IMapper _mapper;

        public CredentialsController(ICredentialService credentialService, IMapper mapper)
        {
            _credentialService = credentialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCredentials()
        {
            try
            {
                var credentials = await _credentialService.GetAllAsync();
                return Ok(credentials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні облікових даних: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCredential(int id)
        {
            try
            {
                var credential = await _credentialService.GetByIdAsync(id);
                if (credential == null)
                    return NotFound("Облікові дані з таким ID не знайдено.");
                return Ok(credential);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні облікових даних: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCredential([FromBody] CredentialDto dto)
        {
            try
            {
                var credential = await _credentialService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetCredential), new { id = credential.Id }, credential);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при створенні облікових даних: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCredential(int id, [FromBody] CredentialDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID облікових даних у URL не відповідає ID у тілі запиту.");
                await _credentialService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Облікові дані з таким ID не знайдено.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при оновленні облікових даних: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCredential(int id)
        {
            try
            {
                await _credentialService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Облікові дані з таким ID не знайдено.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при видаленні облікових даних: " + ex.Message);
            }
        }
    }
}