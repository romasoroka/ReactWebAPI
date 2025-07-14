using Microsoft.EntityFrameworkCore;
using React.ProjectService.Application.IRepositories;
using React.ProjectService.Domain.Entities;
using React.ProjectService.Infrastructure.Data;

namespace React.ProjectService.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Include(p => p.Credentials)
            .Include(p => p.ProjectEmployees)
            .Include(p => p.ProjectTechnologies)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Projects
            .Include(p => p.Credentials)
            .Include(p => p.ProjectEmployees)
            .Include(p => p.ProjectTechnologies)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Project project, CancellationToken cancellationToken)
    {
        await _context.Projects.AddAsync(project, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var project = await _context.Projects.FindAsync(new object[] { id }, cancellationToken);
        if (project != null)
        {
            _context.Projects.Remove(project);
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}