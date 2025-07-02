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
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly AppDbContext _context;

        public TechnologyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Technology>> GetAllAsync()
        {
            return await _context.Technologies
                .Select(t => new Technology { Id = t.Id, Name = t.Name })
                .ToListAsync();
        }

        public async Task<Technology> GetByIdAsync(int id)
        {
            return await _context.Technologies.FindAsync(id);
        }

        public async Task<Technology> GetByNameAsync(string name)
        {
            return await _context.Technologies
                .FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<List<Technology>> GetByNamesAsync(IEnumerable<string> names)
        {
            return await _context.Technologies
                .Where(t => names.Contains(t.Name))
                .ToListAsync();
        }

        public async Task AddAsync(Technology technology)
        {
            _context.Technologies.Add(technology);
        }

        public async Task UpdateAsync(Technology technology)
        {
            _context.Entry(technology).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var technology = await _context.Technologies.FindAsync(id);
            if (technology != null)
            {
                _context.Technologies.Remove(technology);
            }
        }
    }
}
