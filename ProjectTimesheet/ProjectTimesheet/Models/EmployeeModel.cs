namespace ProjectTimesheet.Models
{
    public class EmployeeModel
    {
        public Guid IdEmployee { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Role { get; set; } 
        public string? Department { get; set; }
    }
}
