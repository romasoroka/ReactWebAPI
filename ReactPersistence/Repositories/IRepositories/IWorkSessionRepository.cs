using ReactDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactPersistence.Repositories.IRepositories
{
    public interface IWorkSessionRepository
    {
        Task<IEnumerable<WorkSession>> GetAllAsync();
        Task<WorkSession> GetByIdAsync(int id);
        Task AddAsync(WorkSession workSession);
        Task UpdateAsync(WorkSession workSession);
        Task DeleteAsync(int id);
    }
}
