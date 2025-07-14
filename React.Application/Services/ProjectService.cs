using AutoMapper;
using React.Application.Dtos;
using React.Application.IRepositories;
using React.Application.Services.IServices;
using React.Domain.Entities;
using Shared.Services;
using React.Application.IGrpcClients;


namespace React.Application.Services;

public class ProjectsService : IProjectService
{
    private readonly IProjectGrpcClient _grpcClient;
    private readonly IMapper _mapper;

    public ProjectsService(IProjectGrpcClient grpcClient, IMapper mapper)
    {
        _grpcClient = grpcClient;
        _mapper = mapper;
    }



    public async Task<List<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Executing from React.Application");
        var response = await _grpcClient.GetAllProjectsAsync(cancellationToken);
        return _mapper.Map<List<ProjectDto>>(response.Projects);
    }

    public async Task<ProjectDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await _grpcClient.GetProjectAsync(id, cancellationToken);
        return _mapper.Map<ProjectDto>(response);
    }

    public async Task<ProjectDto> CreateAsync(ProjectDto dto, CancellationToken cancellationToken = default)
    {
        var project = _mapper.Map<ProjectRequest>(dto);
        var response = await _grpcClient.CreateProjectAsync(project, cancellationToken);
        return _mapper.Map<ProjectDto>(response);
    }

    public async Task UpdateAsync(int id, ProjectDto dto, CancellationToken cancellationToken = default)
    {
        var project = _mapper.Map<ProjectRequest>(dto);
        project.Id = id;
        await _grpcClient.UpdateProjectAsync(id, project, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await _grpcClient.DeleteProjectAsync(id, cancellationToken);
    }


}