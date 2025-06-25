using Microsoft.EntityFrameworkCore;

namespace ReactWebAPI.Models
{
    [Owned]
    public class EmployeeStats
    {
        public int HoursWorked { get; set; }
        public int ReportsSubmitted { get; set; }
        public int ProjectsInvolved { get; set; }
    }
}
