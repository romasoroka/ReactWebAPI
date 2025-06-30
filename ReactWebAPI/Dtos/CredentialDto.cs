using System.ComponentModel.DataAnnotations;

namespace ReactWebAPI.Dtos
{
    public class CredentialDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Назва є обов'язковою.")]
        public string Name { get; set; } = null!;
        public string? Value { get; set; }
        public string? Description { get; set; }
    }
}
