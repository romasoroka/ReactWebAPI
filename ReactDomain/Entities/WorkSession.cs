using System.ComponentModel.DataAnnotations;

namespace ReactDomain.Entities
{
    public class WorkSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        [Required]
        public string TaskDescription { get; set; } = null!;
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
