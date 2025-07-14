using AutoMapper;
using React.Application.IRepositories;
using React.Application.Services.IServices;
using React.Domain.Entities;

namespace React.Application.Services;

public class TechnologyService : ITechnologyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TechnologyService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Technology>> GetAllAsync(CancellationToken cancellationToken)
    {
        var technologies = await _unitOfWork.Technologies.GetAllAsync(cancellationToken);
        return _mapper.Map<List<Technology>>(technologies);
    }

    public async Task<Technology?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var technology = await _unitOfWork.Technologies.GetByIdAsync(id, cancellationToken);
        return technology == null ? null : _mapper.Map<Technology>(technology);
    }

    public async Task<Technology> CreateAsync(Technology dto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Назва технології обов'язкова.");

        var existingTech = await _unitOfWork.Technologies.GetByNameAsync(dto.Name, cancellationToken);
        if (existingTech != null)
            throw new ArgumentException($"Технологія з назвою '{dto.Name}' уже існує.");

        var technology = _mapper.Map<Technology>(dto);
        await _unitOfWork.Technologies.AddAsync(technology, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<Technology>(technology);
    }

    public async Task UpdateAsync(int id, Technology dto, CancellationToken cancellationToken)
    {
        var technology = await _unitOfWork.Technologies.GetByIdAsync(id, cancellationToken);
        if (technology == null)
            throw new KeyNotFoundException("Технологію не знайдено.");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Назва технології обов'язкова.");

        var existingTech = await _unitOfWork.Technologies.GetByNameAsync(dto.Name, cancellationToken);
        if (existingTech != null && existingTech.Id != id)
            throw new ArgumentException($"Технологія з назвою '{dto.Name}' уже існує.");

        _mapper.Map(dto, technology);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var technology = await _unitOfWork.Technologies.GetByIdAsync(id, cancellationToken);
        if (technology == null)
            throw new KeyNotFoundException("Технологію не знайдено.");

        await _unitOfWork.Technologies.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

}
