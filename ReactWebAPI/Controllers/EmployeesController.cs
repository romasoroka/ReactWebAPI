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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні працівників: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                    return NotFound("Працівника з таким ID не знайдено.");
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при отриманні працівника: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto dto)
        {
            try
            {
                var employee = await _employeeService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при створенні працівника: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto dto)
        {
            try
            {
                if (id != dto.Id)
                    return BadRequest("ID працівника в URL не відповідає ID у тілі запиту.");
                await _employeeService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Працівника з таким ID не знайдено.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при оновленні працівника: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Працівника з таким ID не знайдено.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Виникла помилка при видаленні працівника: " + ex.Message);
            }
        }
    }
}