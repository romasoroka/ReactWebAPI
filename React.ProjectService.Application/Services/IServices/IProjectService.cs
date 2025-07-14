using React.ProjectService.Application.Dtos;

namespace React.ProjectService.Application.Services.IServices;

public interface IProjectService
{
    Task<IEnumerable<ProjectShortDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProjectDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProjectDto> CreateAsync(ProjectDto dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, ProjectDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}