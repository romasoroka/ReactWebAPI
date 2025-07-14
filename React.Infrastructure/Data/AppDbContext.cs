using Microsoft.EntityFrameworkCore;
using React.Domain.Entities;

namespace React.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
    {
    }
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Credential> Credentials => Set<Credential>();
    public DbSet<Technology> Technologies => Set<Technology>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<WorkSession> WorkSessions => Set<WorkSession>();

}
