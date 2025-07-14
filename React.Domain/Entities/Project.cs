using System.ComponentModel.DataAnnotations;

namespace React.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public ProjectStatus Status { get; set; }
    [Required]
    public string Description { get; set; } = null!;
    public string? DetailedDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal Budget { get; set; }
    [Required]
    public string Client { get; set; } = null!;
    public int TotalHoursLogged { get; set; }
    public int ReportCount { get; set; }
    public int ActiveEmployees { get; set; }
    public List<Technology> Technologies { get; set; } = new List<Technology>();
    public List<Credential> Credentials { get; set; } = new List<Credential>();
    public List<Employee> Employees { get; set; } = new List<Employee>();
}
