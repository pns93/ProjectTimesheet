using System;
using System.Collections.Generic;

namespace ProjectTimesheet.Models.DBObjects
{
    public partial class ProjectTask
    {
        public Guid IdTask { get; set; }
        public Guid IdEmployee { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid IdProject { get; set; }
        public Guid IdTaskType { get; set; }
        public bool? IsApproved { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; } = null!;
        public virtual Project IdProjectNavigation { get; set; } = null!;
        public virtual TaskType IdTaskTypeNavigation { get; set; } = null!;
    }
}
