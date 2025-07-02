using Microsoft.EntityFrameworkCore;
using ReactDomain.Entities;
using ReactPersistence.Data;
using ReactPersistence.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactPersistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Technologies)
                .Include(p => p.Credentials)
                .Include(p => p.Employees)
                .ToListAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Technologies)
                .Include(p => p.Credentials)
                .Include(p => p.Employees)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Project project)
        {
            _context.Projects.Add(project);
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Entry(project).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
        }

        public async Task<List<Project>> GetByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Projects
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }
    }
}
