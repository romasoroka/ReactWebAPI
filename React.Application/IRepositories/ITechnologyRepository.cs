using React.Domain.Entities;


namespace React.Application.IRepositories;

public interface ITechnologyRepository
{
    Task<IEnumerable<Technology>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Technology> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Technology> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<List<Technology>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);
    Task AddAsync(Technology technology, CancellationToken cancellationToken = default);
    Task UpdateAsync(Technology technology, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
