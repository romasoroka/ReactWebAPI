using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactPersistence.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IProjectRepository Projects { get; }
        ICredentialRepository Credentials { get; }
        ITechnologyRepository Technologies { get; }
        IWorkSessionRepository WorkSessions { get; }
        Task<int> SaveChangesAsync();
    }
}
