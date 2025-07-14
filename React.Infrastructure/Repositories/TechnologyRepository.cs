using Microsoft.EntityFrameworkCore;
using React.Application.IRepositories;
using React.Domain.Entities;
using React.Infrastructure.Data;


namespace React.Infrastructure.Repositories;

public class TechnologyRepository : ITechnologyRepository
{
    private readonly AppDbContext _context;

    public TechnologyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Technology>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Technologies
            .Select(t => new Technology { Id = t.Id, Name = t.Name })
            .ToListAsync(cancellationToken);
    }

    public async Task<Technology?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Technologies.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Technology?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Technologies
            .FirstOrDefaultAsync(t => t.Name == name, cancellationToken);
    }

    public async Task<List<Technology>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default)
    {
        return await _context.Technologies
            .Where(t => names.Contains(t.Name))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Technology technology, CancellationToken cancellationToken = default)
    {
        await _context.Technologies.AddAsync(technology, cancellationToken);
    }

    public Task UpdateAsync(Technology technology, CancellationToken cancellationToken = default)
    {
        _context.Entry(technology).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var technology = await _context.Technologies.FindAsync(new object[] { id }, cancellationToken);
        if (technology != null)
        {
            _context.Technologies.Remove(technology);
        }
    }
}
