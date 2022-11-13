using ProjectTimesheet.Models;
using ProjectTimesheet.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ProjectTimesheet.ViewModel
{
    public class ProjectViewModel
    {
        public Guid IdProject { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid IdPm { get; set; }
        public string ProjectManagerName { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public List<ProjectManagerModel> ProjectManagers { get; set; }
        public ProjectViewModel(ProjectModel model, ProjectManagerRepository projectManagerRepository)
        {
            this.IdProject=model.IdProject;
            this.Name = model.Name;
            this.Description = model.Description;
            this.StartDate = model.StartDate;
            this.EndDate = model.EndDate;
            this.IdPm = model.IdPm;
            var projectManager = projectManagerRepository.GetProjectManagerById(model.IdPm);
            this.ProjectManagerName = projectManager.Name;

        }


    }
}
