using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ProjectTimesheet.Data;
using ProjectTimesheet.Models;
using ProjectTimesheet.Models.DBObjects;
using ProjectTimesheet.Repositories;
using ProjectTimesheet.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace ProjectTimesheet.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ProjectTaskController : Controller
    {
        private ProjectTaskRepository projectTaskRepository;
        private EmployeeRepository employeeRepository;
        private ProjectRepository projectRepository;
        private TaskTypeRepository taskTypeRepository;
        public ProjectTaskController(ApplicationDbContext dbcontext)
        {
            projectTaskRepository = new ProjectTaskRepository(dbcontext);
            employeeRepository = new EmployeeRepository(dbcontext);
            projectRepository = new ProjectRepository(dbcontext);
            taskTypeRepository = new TaskTypeRepository(dbcontext);
        }
        // GET: ProjectTaskController
        public async Task<IActionResult> Index(string SortOrder, string SearchString)
        {
            var model = new ProjectTaskRepository();
            var list = projectTaskRepository.GetAllProjectTasks();
            var viewModelList = new List<ProjectTaskViewModel>();
            foreach (var projectTask in list)
            {
                viewModelList.Add(new ProjectTaskViewModel(projectTask, employeeRepository, projectRepository, taskTypeRepository));
            }
            ViewData["CurrentFilter"] = SearchString;
            var projectTasks = from x in viewModelList select x;
            if (!String.IsNullOrEmpty(SearchString))
            {
                projectTasks = projectTasks.Where(x => x.ProjectName.Contains(SearchString));
            }
            ViewData["NameSortParam"] = SortOrder == "Name" ? "name_sort" : "Name";
            ViewData["DateSortParam"] = SortOrder == "Date" ? "date_sort" : "Date";
            ViewData["EmployeeSortParam"] = SortOrder == "EmployeeName" ? "employee_sort" : "EmployeeName";
            ViewData["ProjectSortParam"] = SortOrder == "ProjectName" ? "project_sort" : "ProjectName";
            switch (SortOrder)
            {
                case "Name":
                    projectTasks = projectTasks.OrderBy(x => x.Name);
                    break;
                case "name_sort":
                    projectTasks = projectTasks.OrderByDescending(x => x.Name);
                    break;
                case "EmployeeName":
                    projectTasks = projectTasks.OrderBy(x => x.EmployeeName);
                    break;
                case "employee_sort":
                    projectTasks = projectTasks.OrderByDescending(x => x.EmployeeName);
                    break;
                case "Date":
                    projectTasks = projectTasks.OrderBy(x => x.StartDate);
                    break;
                case "date_sort":
                    projectTasks = projectTasks.OrderByDescending(x => x.StartDate);
                    break;
                case "ProjectName":
                    projectTasks = projectTasks.OrderBy(x => x.ProjectName);
                    break;
                case "project_sort":
                    projectTasks = projectTasks.OrderByDescending(x => x.ProjectName);
                    break;
            }
            return View(projectTasks);
        }


        // GET: ProjectTaskController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = projectTaskRepository.GetProjectTaskById(id);
            var viewModel = new ProjectTaskViewModel(model, employeeRepository, projectRepository, taskTypeRepository);

            return View("DetailsProjectTask", viewModel);
        }

        // GET: ProjectTaskController/Create

        public ActionResult Create()
        {

            var employees = employeeRepository.GetAllEmployees();
            var projects = projectRepository.GetAllProjects();
            var taskTypes = taskTypeRepository.GetAllTaskTypes();
            ViewBag.EmployeeList = employees.Select(x => new SelectListItem(String.Concat(x.FirstName, " ", x.LastName), x.IdEmployee.ToString()));
            ViewBag.ProjectList = projects.Select(x => new SelectListItem(x.Name, x.IdProject.ToString()));
            ViewBag.TaskTypeList = taskTypes.Select(x => new SelectListItem(x.Name, x.IdTaskType.ToString()));
            var model = new ProjectTaskModel();
            var viewModel = new ProjectTaskViewModel(model, employeeRepository, projectRepository, taskTypeRepository);

            return View("CreateProjectTask",viewModel);
        }

        // POST: ProjectTaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ProjectTaskModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    projectTaskRepository.InsertProjectTask(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateProjectTask");
            }
        }

        // GET: ProjectTaskController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var employees = employeeRepository.GetAllEmployees();
            var projects = projectRepository.GetAllProjects();
            var taskTypes = taskTypeRepository.GetAllTaskTypes();
            ViewBag.EmployeeList = employees.Select(x => new SelectListItem(String.Concat(x.FirstName, " ", x.LastName), x.IdEmployee.ToString()));
            ViewBag.ProjectList = projects.Select(x => new SelectListItem(x.Name, x.IdProject.ToString()));
            ViewBag.TaskTypeList = taskTypes.Select(x => new SelectListItem(x.Name, x.IdTaskType.ToString()));

            var model = projectTaskRepository.GetProjectTaskById(id);
            var viewModel = new ProjectTaskViewModel(model, employeeRepository, projectRepository, taskTypeRepository);
            return View("EditProjectTask", viewModel);
        }

        // POST: ProjectTaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new ProjectTaskModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    projectTaskRepository.UpdateProjectTask(model);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditProjectTask", id);
            }

        }

        // GET: ProjectTaskController/Delete/5
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(Guid id)
        {
            var model = projectTaskRepository.GetProjectTaskById(id);
            var viewModel = new ProjectTaskViewModel(model, employeeRepository, projectRepository, taskTypeRepository);
            return View("DeleteProjectTask", viewModel);
        }

        // POST: ProjectTaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            //var model = new ProjectTaskModel();
            try
            {   
                projectTaskRepository.DeleteProjectTask(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
