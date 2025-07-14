using React.Domain.Entities;


namespace React.Application.IRepositories;

public interface IWorkSessionRepository
{
    Task<IEnumerable<WorkSession>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<WorkSession> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(WorkSession workSession, CancellationToken cancellationToken = default);
    Task UpdateAsync(WorkSession workSession, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
