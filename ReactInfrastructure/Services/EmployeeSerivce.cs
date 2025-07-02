using AutoMapper;
using MassTransit;
using ReactApplication.Application.Services;
using ReactApplication.Dtos;
using ReactDomain.Entities;
using ReactPersistence.Repositories.IRepositories;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactInfrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITopicProducer<EmployeeAdded> _producer;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, ITopicProducer<EmployeeAdded> producer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _producer = producer;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var entities = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<List<EmployeeDto>>(entities);
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(id);
            return emp == null ? null : _mapper.Map<EmployeeDto>(emp);
        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("Ім'я працівника обов'язкове.");
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Електронна пошта обов'язкова.");
            if (dto.Skills == null || !dto.Skills.Any())
                throw new ArgumentException("Потрібна хоча б одна навичка.");

            var employee = _mapper.Map<Employee>(dto);

            if (dto.Skills.Any())
            {
                var technologies = await _unitOfWork.Technologies.GetByNamesAsync(dto.Skills);
                if (technologies.Count != dto.Skills.Count)
                    throw new ArgumentException("Деякі назви навичок неправильні або не знайдені.");
                employee.Skills = technologies;
            }

            if (dto.ProjectIds?.Any() == true)
            {
                var projects = await _unitOfWork.Projects.GetByIdsAsync(dto.ProjectIds);
                if (projects.Count != dto.ProjectIds.Count)
                    throw new ArgumentException("Деякі ID проєктів неправильні.");
                employee.Projects = projects;
            }

            await _unitOfWork.Employees.AddAsync(employee);
            await _producer.Produce(new EmployeeAdded(employee.Email));
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task UpdateAsync(int id, EmployeeDto dto)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(id);
            if (emp == null)
                throw new KeyNotFoundException("Працівника не знайдено.");

            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new ArgumentException("Ім'я працівника обов'язкове.");
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Електронна пошта обов'язкова.");
            if (dto.Skills == null || !dto.Skills.Any())
                throw new ArgumentException("Потрібна хоча б одна навичка.");

            _mapper.Map(dto, emp);

            emp.Skills.Clear();
            if (dto.Skills.Any())
            {
                var technologies = await _unitOfWork.Technologies.GetByNamesAsync(dto.Skills);
                if (technologies.Count != dto.Skills.Count)
                    throw new ArgumentException("Деякі навички неправильні.");
                emp.Skills = technologies;
            }

            emp.Projects.Clear();
            if (dto.ProjectIds?.Any() == true)
            {
                var projects = await _unitOfWork.Projects.GetByIdsAsync(dto.ProjectIds);
                if (projects.Count != dto.ProjectIds.Count)
                    throw new ArgumentException("Деякі ID проєктів неправильні.");
                emp.Projects = projects;
            }

            emp.WorkSessions.Clear();
            if (dto.WorkSessions?.Any() == true)
            {
                var workSessions = _mapper.Map<List<WorkSession>>(dto.WorkSessions);
                foreach (var ws in workSessions)
                {
                    ws.EmployeeId = id;
                    var existingSession = await _unitOfWork.WorkSessions.GetByIdAsync(ws.Id);
                    if (existingSession == null)
                    {
                        await _unitOfWork.WorkSessions.AddAsync(ws);
                    }
                    else
                    {
                        await _unitOfWork.WorkSessions.UpdateAsync(ws);
                    }
                    emp.WorkSessions.Add(ws);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var emp = await _unitOfWork.Employees.GetByIdAsync(id);
            if (emp == null)
                throw new KeyNotFoundException("Працівника не знайдено.");

            await _unitOfWork.Employees.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}