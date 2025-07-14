using Microsoft.EntityFrameworkCore;
using React.Application.IRepositories;
using React.Domain.Entities;
using React.Infrastructure.Data;


namespace React.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(IEnumerable<int>? filterIds = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Employees
            .Include(e => e.Projects)
            .Include(e => e.WorkSessions)
            .Include(e => e.Skills)
            .AsQueryable();

        if (filterIds != null && filterIds.Any())
        {
            query = query.Where(e => filterIds.Contains(e.Id));
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .Include(e => e.Projects)
            .Include(e => e.WorkSessions)
            .Include(e => e.Skills)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task AddAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await _context.Employees.AddAsync(employee, cancellationToken);
    }

    public Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        _context.Entry(employee).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _context.Employees.FindAsync(new object[] { id }, cancellationToken);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
        }
    }

    public async Task<List<Technology>> GetTechnologiesByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default)
    {
        return await _context.Technologies
            .Where(t => names.Contains(t.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Project>> GetProjectsByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }
}
