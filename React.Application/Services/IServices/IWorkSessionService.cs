using React.Application.Dtos;

namespace React.Application.Services.IServices;

public interface IWorkSessionService
{
    Task<IEnumerable<WorkSessionDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<WorkSessionDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<WorkSessionDto> CreateAsync(WorkSessionDto dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, WorkSessionDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
