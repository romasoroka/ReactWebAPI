
public class WorkSessionDto
{
    public int? Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string TaskDescription { get; set; } = null!;
    public int ProjectId { get; set; }
    public int EmployeeId { get; set; }
}
