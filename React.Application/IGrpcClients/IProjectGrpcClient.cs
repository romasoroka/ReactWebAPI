using Shared.Services;

namespace React.Application.IGrpcClients;

public interface IProjectGrpcClient
{
    Task<ProjectListRequest> GetAllProjectsAsync(CancellationToken cancellationToken = default);

    Task<ProjectRequest> GetProjectAsync(int id, CancellationToken cancellationToken = default);

    Task<ProjectRequest> CreateProjectAsync(ProjectRequest project, CancellationToken cancellationToken = default);

    Task UpdateProjectAsync(int id, ProjectRequest project, CancellationToken cancellationToken = default);

    Task DeleteProjectAsync(int id, CancellationToken cancellationToken = default);
}