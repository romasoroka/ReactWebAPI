using React.Application.Dtos;

namespace React.Application.Services.IServices;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<EmployeeDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<EmployeeDto> CreateAsync(EmployeeDto dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, EmployeeDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}