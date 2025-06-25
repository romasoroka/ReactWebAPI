namespace ReactWebAPI.Models
{
    public class WorkSession
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Project { get; set; } = string.Empty;
        public int Hours { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
