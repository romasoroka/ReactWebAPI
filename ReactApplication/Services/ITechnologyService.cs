using ReactDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactApplication.Services
{
    public interface ITechnologyService
    {
        Task<IEnumerable<Technology>> GetAllAsync();
        Task<Technology> GetByIdAsync(int id);
        Task<Technology> CreateAsync(Technology dto);
        Task UpdateAsync(int id, Technology dto);
        Task DeleteAsync(int id);
    }
}
