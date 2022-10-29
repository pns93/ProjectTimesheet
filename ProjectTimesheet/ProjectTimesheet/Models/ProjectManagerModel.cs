namespace ProjectTimesheet.Models
{
    public class ProjectManagerModel
    {
        public Guid IdPm { get; set; }
        public string Name { get; set; } = null!;
        public string? Department { get; set; }
    }
}
