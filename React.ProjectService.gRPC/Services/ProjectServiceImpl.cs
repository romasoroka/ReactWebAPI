using AutoMapper;
using Grpc.Core;
using Shared.Services;
using React.ProjectService.Application.Dtos;
using React.ProjectService.Application.Services.IServices;
using static Shared.Services.ProjectService;

namespace React.ProjectService.gRPC.Services
{
    public class ProjectServiceImpl : ProjectServiceBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectServiceImpl(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public override async Task<ProjectListRequest> GetAllProjects(EmptyRequest request, ServerCallContext context)
        {
            Console.WriteLine("Getting from DB...");
            //var ct = context.CancellationToken;
            //var projectsDto = await _projectService.GetAllAsync(ct);
            //var projectList = new ProjectListRequest
            //{
            //    Projects = { projectsDto.Select(dto => _mapper.Map<ProjectRequest>(dto)) }
            //};
            //return projectList;
            return new ProjectListRequest
            {
                Projects = { new ProjectRequest { Id = 1, Name = "Test" } }
            };

        }

        public override async Task<ProjectRequest> GetProject(ProjectIdRequest request, ServerCallContext context)
        {
            if (request == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Project ID is required"));
            var ct = context.CancellationToken;
            var projectDto = await _projectService.GetByIdAsync(request.Id, ct);
            if (projectDto == null) throw new RpcException(new Status(StatusCode.NotFound, "Project not found"));
            return _mapper.Map<ProjectRequest>(projectDto);
        }

        public override async Task<ProjectRequest> CreateProject(ProjectRequest request, ServerCallContext context)
        {
            if (request == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Project data is required"));
            var ct = context.CancellationToken;
            var projectDto = _mapper.Map<ProjectDto>(request);
            var createdProject = await _projectService.CreateAsync(projectDto, ct);
            return _mapper.Map<ProjectRequest>(createdProject);
        }

        public override async Task<EmptyRequest> UpdateProject(ProjectRequest request, ServerCallContext context)
        {
            if (request == null || request.Id <= 0) throw new RpcException(new Status(StatusCode.InvalidArgument, "Valid project ID is required"));
            var ct = context.CancellationToken;
            var projectDto = _mapper.Map<ProjectDto>(request);
            await _projectService.UpdateAsync(request.Id, projectDto, ct);
            return new EmptyRequest();
        }

        public override async Task<EmptyRequest> DeleteProject(ProjectIdRequest request, ServerCallContext context)
        {
            if (request == null || request.Id <= 0) throw new RpcException(new Status(StatusCode.InvalidArgument, "Valid project ID is required"));
            var ct = context.CancellationToken;
            await _projectService.DeleteAsync(request.Id, ct);
            return new EmptyRequest();
        }
    }
}