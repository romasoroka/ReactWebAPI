using AutoMapper;
using ReactApplication.Dtos;
using ReactDomain.Entities;
using ReactPersistence.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactApplication.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _unitOfWork.Projects.GetAllAsync();
            return _mapper.Map<List<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetByIdAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> CreateAsync(ProjectDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва проєкту обов'язкова.");

            var project = _mapper.Map<Project>(dto);

            if (dto.EmployeeIds?.Any() == true)
            {
                if (dto.EmployeeIds.Any(id => id <= 0))
                    throw new ArgumentException("ID працівників повинні бути більше 0.");
                var employees = (await _unitOfWork.Employees.GetAllAsync(dto.EmployeeIds)).ToList();
                if (employees.Count != dto.EmployeeIds.Count)
                    throw new ArgumentException("Деякі ID працівників неправильні.");
                project.Employees = employees;
            }

            if (dto.Technologies?.Any() == true)
            {
                var technologies = new List<Technology>();
                foreach (var techName in dto.Technologies)
                {
                    var tech = await _unitOfWork.Technologies.GetByNameAsync(techName);
                    if (tech == null)
                    {
                        tech = new Technology { Name = techName };
                        await _unitOfWork.Technologies.AddAsync(tech);
                    }
                    technologies.Add(tech);
                }
                project.Technologies = technologies;
            }

            if (dto.Credentials?.Any() == true)
            {
                var credentials = new List<Credential>();
                foreach (var credDto in dto.Credentials)
                {
                    var credential = await _unitOfWork.Credentials.GetByNameAndValueAsync(credDto.Name, credDto.Value);
                    if (credential == null)
                    {
                        credential = _mapper.Map<Credential>(credDto);
                        await _unitOfWork.Credentials.AddAsync(credential);
                    }
                    credentials.Add(credential);
                }
                project.Credentials = credentials;
            }

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task UpdateAsync(int id, ProjectDto dto)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new KeyNotFoundException("Проєкт не знайдено.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва проєкту обов'язкова.");

            _mapper.Map(dto, project);

            project.Employees.Clear();
            if (dto.EmployeeIds?.Any() == true)
            {
                if (dto.EmployeeIds.Any(id => id <= 0))
                    throw new ArgumentException("ID працівників повинні бути більше 0.");
                var employees = (await _unitOfWork.Employees.GetAllAsync(dto.EmployeeIds)).ToList();
                if (employees.Count != dto.EmployeeIds.Count)
                    throw new ArgumentException("Деякі ID працівників неправильні.");
                project.Employees = employees;
            }

            project.Technologies.Clear();
            if (dto.Technologies?.Any() == true)
            {
                var technologies = new List<Technology>();
                foreach (var techName in dto.Technologies)
                {
                    var tech = await _unitOfWork.Technologies.GetByNameAsync(techName);
                    if (tech == null)
                    {
                        tech = new Technology { Name = techName };
                        await _unitOfWork.Technologies.AddAsync(tech);
                    }
                    technologies.Add(tech);
                }
                project.Technologies = technologies;
            }

            project.Credentials.Clear();
            if (dto.Credentials?.Any() == true)
            {
                var credentials = new List<Credential>();
                foreach (var credDto in dto.Credentials)
                {
                    var credential = await _unitOfWork.Credentials.GetByNameAndValueAsync(credDto.Name, credDto.Value);
                    if (credential == null)
                    {
                        credential = _mapper.Map<Credential>(credDto);
                        await _unitOfWork.Credentials.AddAsync(credential);
                    }
                    credentials.Add(credential);
                }
                project.Credentials = credentials;
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new KeyNotFoundException("Проєкт не знайдено.");

            await _unitOfWork.Projects.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}