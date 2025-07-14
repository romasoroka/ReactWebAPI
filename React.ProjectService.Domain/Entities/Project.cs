using System.ComponentModel.DataAnnotations;

namespace React.ProjectService.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ProjectStatus Status { get; set; }
    public string Description { get; set; } = null!;
    public string? DetailedDescription { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public double Budget { get; set; }
    public string Client { get; set; } = null!;
    public int TotalHoursLogged { get; set; }
    public int ReportCount { get; set; }
    public int ActiveEmployees { get; set; }

    public List<Credential> Credentials { get; set; } = new();
    public List<ProjectTechnology> ProjectTechnologies { get; set; } = new();
    public List<ProjectEmployee> ProjectEmployees { get; set; } = new();
}