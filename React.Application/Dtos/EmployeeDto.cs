﻿namespace React.Application.Dtos;

public class EmployeeDto
{
    public int? Id { get; set; }
    public string FullName { get; set; } = null!;
    public int YearsOfExperience { get; set; }
    public List<string> Skills { get; set; } = new List<string>();
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    public int TotalHoursWorked { get; set; }
    public int ReportsSubmitted { get; set; }
    public int ProjectsInvolved { get; set; }
    public List<WorkSessionDto> WorkSessions { get; set; }
    public List<int> ProjectIds { get; set; } = new();
}
