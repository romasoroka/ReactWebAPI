namespace React.Application.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }
    IProjectRepository Projects { get; }
    ICredentialRepository Credentials { get; }
    ITechnologyRepository Technologies { get; }
    IWorkSessionRepository WorkSessions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
