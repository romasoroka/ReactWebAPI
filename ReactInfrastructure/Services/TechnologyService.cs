using AutoMapper;
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
    public class TechnologyService : ITechnologyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TechnologyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Technology>> GetAllAsync()
        {
            var technologies = await _unitOfWork.Technologies.GetAllAsync();
            return _mapper.Map<List<Technology>>(technologies);
        }

        public async Task<Technology> GetByIdAsync(int id)
        {
            var technology = await _unitOfWork.Technologies.GetByIdAsync(id);
            return technology == null ? null : _mapper.Map<Technology>(technology);
        }

        public async Task<Technology> CreateAsync(Technology dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва технології обов'язкова.");

            var existingTech = await _unitOfWork.Technologies.GetByNameAsync(dto.Name);
            if (existingTech != null)
                throw new ArgumentException($"Технологія з назвою '{dto.Name}' уже існує.");

            var technology = _mapper.Map<Technology>(dto);
            await _unitOfWork.Technologies.AddAsync(technology);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Technology>(technology);
        }

        public async Task UpdateAsync(int id, Technology dto)
        {
            var technology = await _unitOfWork.Technologies.GetByIdAsync(id);
            if (technology == null)
                throw new KeyNotFoundException("Технологію не знайдено.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва технології обов'язкова.");

            var existingTech = await _unitOfWork.Technologies.GetByNameAsync(dto.Name);
            if (existingTech != null && existingTech.Id != id)
                throw new ArgumentException($"Технологія з назвою '{dto.Name}' уже існує.");

            _mapper.Map(dto, technology);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var technology = await _unitOfWork.Technologies.GetByIdAsync(id);
            if (technology == null)
                throw new KeyNotFoundException("Технологію не знайдено.");

            await _unitOfWork.Technologies.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
