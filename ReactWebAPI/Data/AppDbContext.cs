using Microsoft.EntityFrameworkCore;
using ReactWebAPI.Models;

namespace ReactWebAPI.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<WorkSession> WorkSessions { get; set; }

    }
}
