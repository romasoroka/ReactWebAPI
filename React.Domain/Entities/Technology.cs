using System.ComponentModel.DataAnnotations;

namespace React.Domain.Entities;

public class Technology
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public List<Project> Projects { get; set; } = new List<Project>();
    public List<Employee> Employees { get; set; } = new List<Employee>();
}
