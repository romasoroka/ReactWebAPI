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
    public class CredentialRepository : ICredentialRepository
    {
        private readonly AppDbContext _context;

        public CredentialRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Credential>> GetAllAsync()
        {
            return await _context.Credentials.ToListAsync();
        }

        public async Task<Credential> GetByIdAsync(int id)
        {
            return await _context.Credentials.FindAsync(id);
        }

        public async Task<Credential> GetByNameAndValueAsync(string name, string value)
        {
            return await _context.Credentials
                .FirstOrDefaultAsync(c => c.Name == name && c.Value == value);
        }

        public async Task AddAsync(Credential credential)
        {
            _context.Credentials.Add(credential);
        }

        public async Task UpdateAsync(Credential credential)
        {
            _context.Entry(credential).State = EntityState.Modified;
        }

        public async Task DeleteAsync(int id)
        {
            var credential = await _context.Credentials.FindAsync(id);
            if (credential != null)
            {
                _context.Credentials.Remove(credential);
            }
        }
    }
}
