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
    public class CredentialService : ICredentialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CredentialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CredentialDto>> GetAllAsync()
        {
            var credentials = await _unitOfWork.Credentials.GetAllAsync();
            return _mapper.Map<List<CredentialDto>>(credentials);
        }

        public async Task<CredentialDto> GetByIdAsync(int id)
        {
            var credential = await _unitOfWork.Credentials.GetByIdAsync(id);
            return credential == null ? null : _mapper.Map<CredentialDto>(credential);
        }

        public async Task<CredentialDto> CreateAsync(CredentialDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва облікових даних обов'язкова.");

            var existingCredential = await _unitOfWork.Credentials.GetByNameAndValueAsync(dto.Name, dto.Value);
            if (existingCredential != null)
                throw new ArgumentException($"Облікові дані з назвою '{dto.Name}' і значенням '{dto.Value}' уже існують.");

            var credential = _mapper.Map<Credential>(dto);
            await _unitOfWork.Credentials.AddAsync(credential);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CredentialDto>(credential);
        }

        public async Task UpdateAsync(int id, CredentialDto dto)
        {
            var credential = await _unitOfWork.Credentials.GetByIdAsync(id);
            if (credential == null)
                throw new KeyNotFoundException("Облікові дані не знайдено.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва облікових даних обов'язкова.");

            var existingCredential = await _unitOfWork.Credentials.GetByNameAndValueAsync(dto.Name, dto.Value);
            if (existingCredential != null && existingCredential.Id != id)
                throw new ArgumentException($"Облікові дані з назвою '{dto.Name}' і значенням '{dto.Value}' уже існують.");

            _mapper.Map(dto, credential);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var credential = await _unitOfWork.Credentials.GetByIdAsync(id);
            if (credential == null)
                throw new KeyNotFoundException("Облікові дані не знайдено.");

            await _unitOfWork.Credentials.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
