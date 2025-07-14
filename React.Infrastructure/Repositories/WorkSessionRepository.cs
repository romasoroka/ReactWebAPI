using Microsoft.EntityFrameworkCore;
using React.Application.IRepositories;
using React.Domain.Entities;
using React.Infrastructure.Data;


namespace React.Infrastructure.Repositories;

public class WorkSessionRepository : IWorkSessionRepository
{
    private readonly AppDbContext _context;

    public WorkSessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkSession>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.WorkSessions
            .Include(ws => ws.Project)
            .Include(ws => ws.Employee)
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkSession?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.WorkSessions
            .Include(ws => ws.Project)
            .Include(ws => ws.Employee)
            .FirstOrDefaultAsync(ws => ws.Id == id, cancellationToken);
    }

    public async Task AddAsync(WorkSession workSession, CancellationToken cancellationToken = default)
    {
        await _context.WorkSessions.AddAsync(workSession, cancellationToken);
    }

    public async Task UpdateAsync(WorkSession workSession, CancellationToken cancellationToken = default)
    {
        _context.Entry(workSession).State = EntityState.Modified;
        await Task.CompletedTask; // Оскільки EF Core не має асинхронного методу для Update
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var workSession = await _context.WorkSessions.FindAsync(new object[] { id }, cancellationToken);
        if (workSession != null)
        {
            _context.WorkSessions.Remove(workSession);
        }
    }
}
