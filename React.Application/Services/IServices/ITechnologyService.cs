using React.Domain.Entities;


namespace React.Application.Services.IServices;

public interface ITechnologyService
{
    Task<IEnumerable<Technology>> GetAllAsync(CancellationToken cancellationToken);
    Task<Technology> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Technology> CreateAsync(Technology dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, Technology dto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
