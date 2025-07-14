using React.Domain.Entities;


namespace React.Application.IRepositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync(IEnumerable<int>? filterIds = null, CancellationToken cancellationToken = default);
    Task<Employee> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<List<Technology>> GetTechnologiesByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);
    Task<List<Project>> GetProjectsByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
}
