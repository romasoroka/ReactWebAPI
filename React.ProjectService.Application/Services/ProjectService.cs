using AutoMapper;
using React.ProjectService.Application.Dtos;
using React.ProjectService.Application.IRepositories;
using React.ProjectService.Application.Services.IServices;
using React.ProjectService.Domain.Entities;

namespace React.ProjectService.Application.Services;

public class ProjectsService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectsService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectShortDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        //var projects = await _projectRepository.GetAllAsync(cancellationToken);
        //return _mapper.Map<IEnumerable<ProjectShortDto>>(projects);
        var result = new List<ProjectShortDto>
    {
        new ProjectShortDto
        {
            Id = 1,
            Name = "Test Project 1"
        },
        new ProjectShortDto
        {
            Id = 2,
            Name = "Test Project 2"        
        }
    };

        return await Task.FromResult(result);
    }

    public async Task<ProjectDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(id, cancellationToken);
        return project == null ? null : _mapper.Map<ProjectDto>(project);
    }

    public async Task<ProjectDto> CreateAsync(ProjectDto dto, CancellationToken cancellationToken)
    {
        var project = _mapper.Map<Project>(dto);

        project.ProjectEmployees = dto.EmployeeIds
            .Select(eid => new ProjectEmployee { EmployeeId = eid }).ToList();

        project.ProjectTechnologies = dto.TechnologyIds
            .Select(tid => new ProjectTechnology { TechnologyId = tid }).ToList();

        project.Credentials = _mapper.Map<List<Credential>>(dto.Credentials);

        await _projectRepository.AddAsync(project, cancellationToken);
        await _projectRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task UpdateAsync(int id, ProjectDto dto, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(id, cancellationToken);
        if (project == null) throw new KeyNotFoundException("Проєкт не знайдено.");

        _mapper.Map(dto, project);

        project.ProjectEmployees = dto.EmployeeIds
            .Select(eid => new ProjectEmployee { ProjectId = id, EmployeeId = eid }).ToList();

        project.ProjectTechnologies = dto.TechnologyIds
            .Select(tid => new ProjectTechnology { ProjectId = id, TechnologyId = tid }).ToList();

        project.Credentials = _mapper.Map<List<Credential>>(dto.Credentials);

        await _projectRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(id, cancellationToken);
        if (project == null) throw new KeyNotFoundException("Проєкт не знайдено.");

        await _projectRepository.DeleteAsync(id, cancellationToken);
        await _projectRepository.SaveChangesAsync(cancellationToken);
    }
}
