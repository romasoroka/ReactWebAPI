using System.ComponentModel.DataAnnotations;

namespace React.ProjectService.Domain.Entities;

public class Credential
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Value { get; set; }
    public string? Description { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; } = null!;
}