using System;
using System.Collections.Generic;

namespace ProjectTimesheet.Models.DBObjects
{
    public partial class ProjectManager
    {
        public ProjectManager()
        {
            Projects = new HashSet<Project>();
        }

        public Guid IdPm { get; set; }
        public string Name { get; set; } = null!;
        public string? Department { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
