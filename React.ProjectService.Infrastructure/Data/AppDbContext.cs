using Microsoft.EntityFrameworkCore;
using React.ProjectService.Domain.Entities;

namespace React.ProjectService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Credential> Credentials { get; set; }

    public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
    public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectTechnology>()
            .HasKey(pt => new { pt.ProjectId, pt.TechnologyId });

        modelBuilder.Entity<ProjectEmployee>()
            .HasKey(pe => new { pe.ProjectId, pe.EmployeeId });

        modelBuilder.Entity<Credential>()
            .HasOne(c => c.Project)
            .WithMany(p => p.Credentials)
            .HasForeignKey(c => c.ProjectId);
    }
}
