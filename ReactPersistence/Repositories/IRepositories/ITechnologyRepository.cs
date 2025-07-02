using ReactDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactPersistence.Repositories.IRepositories
{
    public interface ITechnologyRepository
    {
        Task<IEnumerable<Technology>> GetAllAsync();
        Task<Technology> GetByIdAsync(int id);
        Task<Technology> GetByNameAsync(string name);
        Task<List<Technology>> GetByNamesAsync(IEnumerable<string> names);
        Task AddAsync(Technology technology);
        Task UpdateAsync(Technology technology);
        Task DeleteAsync(int id);
    }
}
