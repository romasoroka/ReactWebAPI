using React.Domain.Entities;


namespace React.Application.IRepositories;

public interface ICredentialRepository
{
    Task<IEnumerable<Credential>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Credential> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Credential> GetByNameAndValueAsync(string name, string value, CancellationToken cancellationToken = default);
    Task AddAsync(Credential credential, CancellationToken cancellationToken = default);
    Task UpdateAsync(Credential credential, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
