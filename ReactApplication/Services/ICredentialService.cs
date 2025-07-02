using ReactApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactApplication.Services
{
    public interface ICredentialService
    {
        Task<IEnumerable<CredentialDto>> GetAllAsync();
        Task<CredentialDto> GetByIdAsync(int id);
        Task<CredentialDto> CreateAsync(CredentialDto dto);
        Task UpdateAsync(int id, CredentialDto dto);
        Task DeleteAsync(int id);
    }
}
