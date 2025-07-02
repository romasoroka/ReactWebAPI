using ReactDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactPersistence.Repositories.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(IEnumerable<int>? filterIds = null);
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<List<Technology>> GetTechnologiesByNamesAsync(IEnumerable<string> names);
        Task<List<Project>> GetProjectsByIdsAsync(IEnumerable<int> ids);
    }
}
