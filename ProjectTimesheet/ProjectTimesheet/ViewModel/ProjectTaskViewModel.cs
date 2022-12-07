using ProjectTimesheet.Models;
using ProjectTimesheet.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ProjectTimesheet.ViewModel
{
    public class ProjectTaskViewModel
    {
        public Guid IdTask { get; set; }
        public Guid IdEmployee { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
        public Guid IdProject { get; set; }
        public Guid IdTaskType { get; set; }
        public bool IsApproved { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectName { get; set; }
        public string TaskTypeName { get; set; }
        public List<EmployeeModel> Employees { get; set; }
        public List<ProjectModel> Projects { get; set; }
        public List<TaskTypeModel> TaskTypes { get; set; }
        public ProjectTaskViewModel(ProjectTaskModel model, EmployeeRepository employeeRepository, ProjectRepository projectRepository, TaskTypeRepository taskTypeRepository)
        {
            this.IdTask = model.IdTask;
            this.IdEmployee = model.IdEmployee;
            this.Name = model.Name; 
            this.Description = model.Description;
            this.StartDate = model.StartDate;
            this.EndDate = model.EndDate;
            this.IsApproved = model.IsApproved;
            this.IdProject = model.IdProject;
            this.IdTaskType = model.IdTaskType;
            var employee = employeeRepository.GetEmployeeById(model.IdEmployee);
            var project = projectRepository.GetProjectById(model.IdProject);
            var taskType = taskTypeRepository.GetTaskTypeById(model.IdTaskType);
            this.EmployeeName = String.Concat(employee.FirstName," ",employee.LastName);
            this.ProjectName = project.Name;
            this.TaskTypeName = taskType.Name;
        }
    }
}
