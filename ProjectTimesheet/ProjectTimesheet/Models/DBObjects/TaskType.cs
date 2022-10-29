using System;
using System.Collections.Generic;

namespace ProjectTimesheet.Models.DBObjects
{
    public partial class TaskType
    {
        public TaskType()
        {
            ProjectTasks = new HashSet<ProjectTask>();
        }

        public Guid IdTaskType { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
