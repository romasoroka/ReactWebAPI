using ReactWebAPI.Models;

namespace ReactWebAPI.Dtos
{
    public class ProjectDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public ProjectStatus Status { get; set; }
        public List<string> Technologies { get; set; } = new();
        public string Description { get; set; } = null!;
        public string? DetailedDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }
        public string Client { get; set; } = null!;
        public List<CredentialDto> Credentials { get; set; } = new();
        public int TotalHoursLogged { get; set; } 
        public int ReportCount { get; set; }
        public int ActiveEmployees { get; set; }
        public List<int> EmployeeIds { get; set; } = new();
        public List<string>? EmployeeNames { get; set; }
    }
}
