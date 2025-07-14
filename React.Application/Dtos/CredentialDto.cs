namespace React.Application.Dtos;

public class CredentialDto
{
    public int? Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Value { get; set; }
    public string? Description { get; set; }
}
