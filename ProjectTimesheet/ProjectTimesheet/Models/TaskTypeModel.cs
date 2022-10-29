namespace ProjectTimesheet.Models
{
    public class TaskTypeModel
    {
        public Guid IdTaskType { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
