using System.ComponentModel.DataAnnotations;

namespace ProjectTimesheet.Models
{
    public class ProjectTaskModel
    {
        public Guid IdTask { get; set; }
        public Guid IdEmployee { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        public Guid IdProject { get; set; }
        public Guid IdTaskType { get; set; }
        public bool? IsApproved { get; set; }
    }
}
