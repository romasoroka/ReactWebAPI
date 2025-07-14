using AutoMapper;
using React.Application.Dtos;
using React.Application.IRepositories;
using React.Application.Services.IServices;
using React.Domain.Entities;

namespace React.Application.Services;

public class CredentialService : ICredentialService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CredentialService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CredentialDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var credentials = await _unitOfWork.Credentials.GetAllAsync(cancellationToken);
        return _mapper.Map<List<CredentialDto>>(credentials);
    }

    public async Task<CredentialDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var credential = await _unitOfWork.Credentials.GetByIdAsync(id, cancellationToken);
        return credential == null ? null : _mapper.Map<CredentialDto>(credential);
    }

    public async Task<CredentialDto> CreateAsync(CredentialDto dto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Назва облікових даних обов'язкова.");

        var existingCredential = await _unitOfWork.Credentials.GetByNameAndValueAsync(dto.Name, dto.Value, cancellationToken);
        if (existingCredential != null)
            throw new ArgumentException($"Облікові дані з назвою '{dto.Name}' і значенням '{dto.Value}' уже існують.");

        var credential = _mapper.Map<Credential>(dto);
        await _unitOfWork.Credentials.AddAsync(credential, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CredentialDto>(credential);
    }

    public async Task UpdateAsync(int id, CredentialDto dto, CancellationToken cancellationToken)
    {
        var credential = await _unitOfWork.Credentials.GetByIdAsync(id, cancellationToken);
        if (credential == null)
            throw new KeyNotFoundException("Облікові дані не знайдено.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Назва облікових даних обов'язкова.");

        var existingCredential = await _unitOfWork.Credentials.GetByNameAndValueAsync(dto.Name, dto.Value, cancellationToken);
        if (existingCredential != null && existingCredential.Id != id)
            throw new ArgumentException($"Облікові дані з назвою '{dto.Name}' і значенням '{dto.Value}' уже існують.");

        _mapper.Map(dto, credential);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var credential = await _unitOfWork.Credentials.GetByIdAsync(id, cancellationToken);
        if (credential == null)
            throw new KeyNotFoundException("Облікові дані не знайдено.");

        await _unitOfWork.Credentials.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

}
