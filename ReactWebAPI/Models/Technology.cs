using System.ComponentModel.DataAnnotations;

namespace ReactWebAPI.Models
{
    public class Technology
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
