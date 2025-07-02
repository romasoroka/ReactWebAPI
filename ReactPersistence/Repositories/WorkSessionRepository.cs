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
    public class WorkSessionRepository : IWorkSessionRepository
    {
        private readonly AppDbContext _context;

        public WorkSessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkSession>> GetAllAsync()
        {
            return await _context.WorkSessions
                .Include(ws => ws.Project)
                .Include(ws => ws.Employee)
                .ToListAsync();
        }

        public async Task<WorkSession> GetByIdAsync(int id)
        {
            return await _context.WorkSessions
                .Include(ws => ws.Project)
                .Include(ws => ws.Employee)
                .FirstOrDefaultAsync(ws => ws.Id == id);
        }

        public async Task AddAsync(WorkSession workSession)
        {
            _context.WorkSessions.Add(workSession);
        }

        public async Task UpdateAsync(WorkSession workSession)
        {
            _context.Entry(workSession).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var workSession = await _context.WorkSessions.FindAsync(id);
            if (workSession != null)
            {
                _context.WorkSessions.Remove(workSession);
            }
        }
    }
}
