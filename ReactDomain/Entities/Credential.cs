using System.ComponentModel.DataAnnotations;

namespace ReactDomain.Entities
{
    public class Credential
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
        public string? Description { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
    }

}
