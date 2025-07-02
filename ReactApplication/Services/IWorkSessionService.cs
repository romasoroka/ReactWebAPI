using ReactApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactApplication.Services
{
    public interface IWorkSessionService
    {
        Task<IEnumerable<WorkSessionDto>> GetAllAsync();
        Task<WorkSessionDto> GetByIdAsync(int id);
        Task<WorkSessionDto> CreateAsync(WorkSessionDto dto);
        Task UpdateAsync(int id, WorkSessionDto dto);
        Task DeleteAsync(int id);
    }
}
