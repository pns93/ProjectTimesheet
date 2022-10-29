using System;
using System.Collections.Generic;

namespace ProjectTimesheet.Models.DBObjects
{
    public partial class Project
    {
        public Project()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public Guid IdProject { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid? IdPm { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ProjectManager? IdPmNavigation { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
