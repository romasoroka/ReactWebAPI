using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ReactWebAPI.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public List<string> Technologies { get; set; } = new();
        public string Description { get; set; }
        public List<Employee> Programmers { get; set; } = new();
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Budget { get; set; }
        public string Client { get; set; }
        public string DetailedDescription { get; set; }
        [ValidateNever]
        public List<Credential> Credentials { get; set; }
        public ProjectAnalytics Analytics { get; set; }
    }

}
