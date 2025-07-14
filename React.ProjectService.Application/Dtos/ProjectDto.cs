using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.ProjectService.Application.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ProjectStatus Status { get; set; }
        public List<int> TechnologyIds { get; set; } = new();
        public string Description { get; set; } = null!;
        public string? DetailedDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double Budget { get; set; }
        public string Client { get; set; } = null!;
        public List<CredentialDto> Credentials { get; set; } = new();
        public int TotalHoursLogged { get; set; }
        public int ReportCount { get; set; }
        public int ActiveEmployees { get; set; }
        public List<int> EmployeeIds { get; set; } = new();
    }
}
