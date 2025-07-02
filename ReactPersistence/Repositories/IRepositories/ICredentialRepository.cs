using ReactDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactPersistence.Repositories.IRepositories
{
    public interface ICredentialRepository
    {
        Task<IEnumerable<Credential>> GetAllAsync();
        Task<Credential> GetByIdAsync(int id);
        Task<Credential> GetByNameAndValueAsync(string name, string value);
        Task AddAsync(Credential credential);
        Task UpdateAsync(Credential credential);
        Task DeleteAsync(int id);
    }
}
