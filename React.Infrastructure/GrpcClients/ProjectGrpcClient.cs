using Grpc.Net.Client;
using Shared.Services;
using React.Application.IGrpcClients;
using static Shared.Services.ProjectService;


namespace React.Infrastructure.GrpcClients
{
    public class ProjectGrpcClient : IProjectGrpcClient
    {
        private readonly ProjectServiceClient _client;

        public ProjectGrpcClient(string grpcServerUrl)
        {
            if (string.IsNullOrEmpty(grpcServerUrl))
                throw new ArgumentException("gRPC server URL is required.", nameof(grpcServerUrl));
            Console.WriteLine($"ProjectGrpcClient initialized with URL: {grpcServerUrl}");
            var channel = GrpcChannel.ForAddress(grpcServerUrl);
            _client = new ProjectServiceClient(channel);
        }

        public async Task<ProjectListRequest> GetAllProjectsAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Getting from grpc...");
            return await _client.GetAllProjectsAsync(new EmptyRequest(), cancellationToken: cancellationToken);
        }

        public async Task<ProjectRequest> GetProjectAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Valid project ID is required.", nameof(id));
            return await _client.GetProjectAsync(new ProjectIdRequest { Id = id }, cancellationToken: cancellationToken);
        }

        public async Task<ProjectRequest> CreateProjectAsync(ProjectRequest project, CancellationToken cancellationToken = default)
        {
            if (project == null) throw new ArgumentNullException(nameof(project));
            return await _client.CreateProjectAsync(project, cancellationToken: cancellationToken);
        }

        public async Task UpdateProjectAsync(int id, ProjectRequest project, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Valid project ID is required.", nameof(id));
            if (project == null) throw new ArgumentNullException(nameof(project));
            await _client.UpdateProjectAsync(project, cancellationToken: cancellationToken);
        }

        public async Task DeleteProjectAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0) throw new ArgumentException("Valid project ID is required.", nameof(id));
            await _client.DeleteProjectAsync(new ProjectIdRequest { Id = id }, cancellationToken: cancellationToken);
        }
    }
}
