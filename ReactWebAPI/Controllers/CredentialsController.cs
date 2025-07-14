using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using React.Application.Dtos;
using React.Application.Services.IServices;

namespace ReactWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CredentialsController : ControllerBase
{
    private readonly ICredentialService _credentialService;

    public CredentialsController(ICredentialService credentialService)
    {
        _credentialService = credentialService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCredentials(CancellationToken cancellationToken)
    {
        var credentials = await _credentialService.GetAllAsync(cancellationToken);
        return Ok(credentials);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCredential(int id, CancellationToken cancellationToken)
    {
        var credential = await _credentialService.GetByIdAsync(id, cancellationToken);
        if (credential == null)
            return NotFound("Облікові дані з таким ID не знайдено.");
        return Ok(credential);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCredential([FromBody] CredentialDto dto, CancellationToken cancellationToken)
    {
        var credential = await _credentialService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetCredential), new { id = credential.Id }, credential);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCredential(int id, [FromBody] CredentialDto dto, CancellationToken cancellationToken)
    {
        if (id != dto.Id)
            return BadRequest("ID облікових даних у URL не відповідає ID у тілі запиту.");
        await _credentialService.UpdateAsync(id, dto, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCredential(int id, CancellationToken cancellationToken)
    {
        await _credentialService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}