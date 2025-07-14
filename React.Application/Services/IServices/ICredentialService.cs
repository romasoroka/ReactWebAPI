using React.Application.Dtos;


namespace React.Application.Services.IServices;

public interface ICredentialService
{
    Task<IEnumerable<CredentialDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<CredentialDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<CredentialDto> CreateAsync(CredentialDto dto, CancellationToken cancellationToken);
    Task UpdateAsync(int id, CredentialDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}
