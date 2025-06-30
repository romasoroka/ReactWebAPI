using System.ComponentModel.DataAnnotations;

namespace ReactWebAPI.Dtos
{
    public class WorkSessionDto
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        [Required(ErrorMessage = "Опис завдання є обов'язковим.")]
        public string TaskDescription { get; set; } = null!;
        [Range(1, int.MaxValue, ErrorMessage = "ProjectId повинен бути більше 0.")]
        public int ProjectId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "EmployeeId повинен бути більше 0.")]
        public int EmployeeId { get; set; }
    }
}
