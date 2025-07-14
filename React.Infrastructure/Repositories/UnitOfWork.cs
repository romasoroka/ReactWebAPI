using React.Application.IRepositories;
using React.Infrastructure.Data;


namespace React.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IEmployeeRepository Employees { get; }
    public IProjectRepository Projects { get; }
    public ICredentialRepository Credentials { get; }
    public ITechnologyRepository Technologies { get; }
    public IWorkSessionRepository WorkSessions { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Employees = new EmployeeRepository(context);
        Projects = new ProjectRepository(context);
        Credentials = new CredentialRepository(context);
        Technologies = new TechnologyRepository(context);
        WorkSessions = new WorkSessionRepository(context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
