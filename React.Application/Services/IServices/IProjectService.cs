using React.Application.Dtos;

namespace React.Application.Services.IServices;

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProjectDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ProjectDto> CreateAsync(ProjectDto dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, ProjectDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}