using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public ActionResult Index()
        {
            var list = projectTaskRepository.GetAllProjectTasks();
            var viewModelList = new List<ProjectTaskViewModel>();
            foreach (var projectTask in list)
            {
                viewModelList.Add(new ProjectTaskViewModel(projectTask, employeeRepository, projectRepository, taskTypeRepository));

            }
            return View(viewModelList);

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
