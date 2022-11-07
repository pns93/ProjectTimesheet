namespace ProjectTimesheet.Models.DBObjects
{
    public class ProjectTaskViewModel
    {
        public Guid IdTask { get; set; }
        public Guid IdEmployee { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid IdProject { get; set; }
        public Guid IdTaskType { get; set; }
        public bool IsApproved { get; set; }
    }
}
