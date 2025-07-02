using ReactApplication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApplication.Application.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<EmployeeDto> CreateAsync(EmployeeDto dto);
        Task UpdateAsync(int id, EmployeeDto dto);
        Task DeleteAsync(int id);
    }
}