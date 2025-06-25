using Microsoft.EntityFrameworkCore;

namespace ReactWebAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public List<string> Skills { get; set; } = new List<string>();
        public int Experience { get; set; }
        public List<string> Projects { get; set; } = new List<string>();
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public EmployeeStats Stats { get; set; } = new EmployeeStats();
        public List<WorkSession> RecentWorkSessions { get; set; } = new List<WorkSession>();
    }
}

