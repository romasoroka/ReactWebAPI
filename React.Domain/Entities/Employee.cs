using System.ComponentModel.DataAnnotations;

namespace React.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    [Required]
    public string FullName { get; set; } = null!;
    public int YearsOfExperience { get; set; }
    [Required]
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    public int TotalHoursWorked { get; set; }
    public int ReportsSubmitted { get; set; }
    public int ProjectsInvolved { get; set; }
    public List<Project> Projects { get; set; } = new List<Project>();
    public List<Technology> Skills { get; set; } = new List<Technology>();
    public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();
}

