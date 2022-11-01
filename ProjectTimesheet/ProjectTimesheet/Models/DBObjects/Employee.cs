using System;
using System.Collections.Generic;

namespace ProjectTimesheet.Models.DBObjects
{
    public partial class Employee
    {
        public Employee()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public Guid IdEmployee { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Department { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
