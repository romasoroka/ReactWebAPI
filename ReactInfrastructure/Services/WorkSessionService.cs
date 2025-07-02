using AutoMapper;
using ReactApplication.Dtos;
using ReactApplication.Services;
using ReactDomain.Entities;
using ReactPersistence.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactInfrastructure.Services
{
    public class WorkSessionService : IWorkSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WorkSessionDto>> GetAllAsync()
        {
            var workSessions = await _unitOfWork.WorkSessions.GetAllAsync();
            return _mapper.Map<List<WorkSessionDto>>(workSessions);
        }

        public async Task<WorkSessionDto> GetByIdAsync(int id)
        {
            var workSession = await _unitOfWork.WorkSessions.GetByIdAsync(id);
            return workSession == null ? null : _mapper.Map<WorkSessionDto>(workSession);
        }

        public async Task<WorkSessionDto> CreateAsync(WorkSessionDto dto)
        {
            if (dto.ProjectId <= 0)
                throw new ArgumentException("ID проєкту повинен бути більше 0.");
            if (dto.EmployeeId <= 0)
                throw new ArgumentException("ID працівника повинен бути більше 0.");
            if (dto.StartTime == default || dto.EndTime == default)
                throw new ArgumentException("Дати початку та завершення обов'язкові.");
            if (dto.EndTime < dto.StartTime)
                throw new ArgumentException("Дата завершення не може бути раніше дати початку.");

            var project = await _unitOfWork.Projects.GetByIdAsync(dto.ProjectId);
            if (project == null)
                throw new KeyNotFoundException("Проєкт не знайдено.");

            var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
                throw new KeyNotFoundException("Працівника не знайдено.");

            var workSession = _mapper.Map<WorkSession>(dto);
            await _unitOfWork.WorkSessions.AddAsync(workSession);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<WorkSessionDto>(workSession);
        }

        public async Task UpdateAsync(int id, WorkSessionDto dto)
        {
            var workSession = await _unitOfWork.WorkSessions.GetByIdAsync(id);
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

            var project = await _unitOfWork.Projects.GetByIdAsync(dto.ProjectId);
            if (project == null)
                throw new KeyNotFoundException("Проєкт не знайдено.");

            var employee = await _unitOfWork.Employees.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
                throw new KeyNotFoundException("Працівника не знайдено.");

            _mapper.Map(dto, workSession);
            await _unitOfWork.WorkSessions.UpdateAsync(workSession);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var workSession = await _unitOfWork.WorkSessions.GetByIdAsync(id);
            if (workSession == null)
                throw new KeyNotFoundException("Робочу сесію не знайдено.");

            await _unitOfWork.WorkSessions.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
