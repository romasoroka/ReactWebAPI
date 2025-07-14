using Microsoft.EntityFrameworkCore;
using React.Application.IRepositories;
using React.Domain.Entities;
using React.Infrastructure.Data;


namespace React.Infrastructure.Repositories;

public class CredentialRepository : ICredentialRepository
{
    private readonly AppDbContext _context;

    public CredentialRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Credential>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Credentials.ToListAsync(cancellationToken);
    }

    public async Task<Credential> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Credentials.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Credential> GetByNameAndValueAsync(string name, string value, CancellationToken cancellationToken = default)
    {
        return await _context.Credentials
            .FirstOrDefaultAsync(c => c.Name == name && c.Value == value, cancellationToken);
    }

    public async Task AddAsync(Credential credential, CancellationToken cancellationToken = default)
    {
        await _context.Credentials.AddAsync(credential, cancellationToken);
    }

    public Task UpdateAsync(Credential credential, CancellationToken cancellationToken = default)
    {
        _context.Entry(credential).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var credential = await _context.Credentials.FindAsync(new object[] { id }, cancellationToken);
        if (credential != null)
        {
            _context.Credentials.Remove(credential);
        }
    }
}
