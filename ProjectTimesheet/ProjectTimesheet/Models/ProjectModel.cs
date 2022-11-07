using System.ComponentModel.DataAnnotations;

namespace ProjectTimesheet.Models
{
    public class ProjectModel
    {
        public Guid IdProject { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid? IdPm { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
