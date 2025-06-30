using System.ComponentModel.DataAnnotations;

namespace ReactWebAPI.Dtos
{
    public class EmployeeDto
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Ім'я є обов'язковим.")]
        public string FullName { get; set; } = null!;
        [Range(0, int.MaxValue, ErrorMessage = "Роки досвіду не можуть бути від'ємними.")]
        public int YearsOfExperience { get; set; }
        public List<string> Skills { get; set; } = new();
        [Required(ErrorMessage = "Email є обов'язковим.")]
        [EmailAddress(ErrorMessage = "Невалідний формат email.")]
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public int TotalHoursWorked { get; set; }
        public int ReportsSubmitted { get; set; }
        public int ProjectsInvolved { get; set; }
        public List<WorkSessionDto> WorkSessions {  get; set; }
        public List<int> ProjectIds { get; set; } = new();
    }
}
