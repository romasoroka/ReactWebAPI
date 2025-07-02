using ReactApplication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactApplication.Application.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto> GetByIdAsync(int id);
        Task<ProjectDto> CreateAsync(ProjectDto dto);
        Task UpdateAsync(int id, ProjectDto dto);
        Task DeleteAsync(int id);
    }
}