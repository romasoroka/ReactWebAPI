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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(IEnumerable<int>? filterIds = null)
        {
            var query = _context.Employees
                .Include(e => e.Projects)
                .Include(e => e.WorkSessions)
                .Include(e => e.Skills)
                .AsQueryable();

            if (filterIds != null && filterIds.Any())
            {
                query = query.Where(e => filterIds.Contains(e.Id));
            }

            return await query.ToListAsync();
        }


        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Projects)
                .Include(e => e.WorkSessions)
                .Include(e => e.Skills)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }
        }

        public async Task<List<Technology>> GetTechnologiesByNamesAsync(IEnumerable<string> names)
        {
            return await _context.Technologies
                .Where(t => names.Contains(t.Name))
                .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Projects
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }
    }
}
