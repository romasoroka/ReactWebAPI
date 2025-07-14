using AutoMapper;
using React.Application.Dtos;
using React.Application.IRepositories;
using React.Application.Services.IServices;
using React.Domain.Entities;

namespace React.Application.Services;

public class WorkSessionService : IWorkSessionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WorkSessionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkSessionDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var workSessions = await _unitOfWork.WorkSessions.GetAllAsync(cancellationToken);
        return _mapper.Map<List<WorkSessionDto>>(workSessions);
    }

    public async Task<WorkSessionDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var workSession = await _unitOfWork.WorkSessions.GetByIdAsync(id, cancellationToken);
        return workSession == null ? null : _mapper.Map<WorkSessionDto>(workSession);
    }

    public async Task<WorkSessionDto> CreateAsync(WorkSessionDto dto, CancellationToken cancellationToken)
    {
        if (dto.ProjectId <= 0)
            throw new ArgumentException("ID проєкту повинен бути більше 0.");
        if (dto.EmployeeId <= 0)
            throw new ArgumentException("ID працівника повинен бути більше 0.");
        if (dto.StartTime == default || dto.EndTime == default)
            throw new ArgumentException("Дати початку та завершення обов'язкові.");
        if (dto.EndTime < dto.StartTime)
            throw new ArgumentException("Дата завершення не може бути раніше дати початку.");

        var project = await _unitOfWork.Projects.GetByIdAsync(dto.ProjectId, cancellationToken);
        if (project == null)
            throw new KeyNotFoundException("Проєкт не знайдено.");

        var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmployeeId, cancellationToken);
        if (employee == null)
            throw new KeyNotFoundException("Працівника не знайдено.");

        var workSession = _mapper.Map<WorkSession>(dto);
        await _unitOfWork.WorkSessions.AddAsync(workSession, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<WorkSessionDto>(workSession);
    }

    public async Task UpdateAsync(int id, WorkSessionDto dto, CancellationToken cancellationToken)
    {
        var workSession = await _unitOfWork.WorkSessions.GetByIdAsync(id, cancellationToken);
        if (workSession == null)
            throw new KeyNotFoundException("Робочу сесію не знайдено.");

        if (dto.ProjectId <= 0)
            throw new ArgumentException("ID проєкту повинен бути більше 0.");
        if (dto.EmployeeId <= 0)
            throw new ArgumentException("ID працівника повинен бути більше 0.");
        if (dto.StartTime == default || dto.EndTime == default)
            throw new ArgumentException("Дати початку та завершення обов'язкові.");
        if (dto.EndTime < dto.StartTime)
            throw new ArgumentException("Дата завершення не може бути раніше дати початку.");

        var project = await _unitOfWork.Projects.GetByIdAsync(dto.ProjectId, cancellationToken);
        if (project == null)
            throw new KeyNotFoundException("Проєкт не знайдено.");

        var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmployeeId, cancellationToken);
        if (employee == null)
            throw new KeyNotFoundException("Працівника не знайдено.");

        _mapper.Map(dto, workSession);
        await _unitOfWork.WorkSessions.UpdateAsync(workSession, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var workSession = await _unitOfWork.WorkSessions.GetByIdAsync(id, cancellationToken);
        if (workSession == null)
            throw new KeyNotFoundException("Робочу сесію не знайдено.");

        await _unitOfWork.WorkSessions.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

}
