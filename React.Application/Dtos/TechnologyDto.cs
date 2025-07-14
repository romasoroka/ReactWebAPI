namespace React.Application.Dtos;

public class TechnologyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<int> ProjectIds { get; set; } = new();
    public List<int> EmployeeIds { get; set; } = new();
}
