using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectTimesheet.Data;
using ProjectTimesheet.Models;
using ProjectTimesheet.Repositories;
using ProjectTimesheet.ViewModel;
using System.Data;

namespace ProjectTimesheet.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectRepository projectRepository;
        private ProjectManagerRepository projectManagerRepository;
        public ProjectController(ApplicationDbContext dbcontext)
        {
            projectRepository = new ProjectRepository(dbcontext);
            projectManagerRepository = new ProjectManagerRepository(dbcontext);

        }

        // GET: ProjectController
        public ActionResult Index()
        {
            var list = projectRepository.GetAllProjects();
            var viewModelList = new List<ProjectViewModel>();
            foreach (var project in list)
            {
                viewModelList.Add(new ProjectViewModel(project, projectManagerRepository));

            }
            return View(viewModelList);

        }

        // GET: ProjectController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = projectRepository.GetProjectById(id);
            var viewModel = new ProjectViewModel(model, projectManagerRepository);

            return View("DetailsProject", viewModel);
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            var projectManagers = projectManagerRepository.GetAllProjectManagers();
            ViewBag.ProjectManagerList = projectManagers.Select(x => new SelectListItem(x.Name, x.IdPm.ToString()));
            var model = new ProjectModel();
            var viewModel = new ProjectViewModel(model, projectManagerRepository);

            return View("CreateProject", viewModel);
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ProjectModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    projectRepository.InsertProject(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateProject");
            }
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var projectManagers = projectManagerRepository.GetAllProjectManagers();
            ViewBag.ProjectManagerList = projectManagers.Select(x => new SelectListItem(x.Name, x.IdPm.ToString()));

            var model = projectRepository.GetProjectById(id);
            var viewModel = new ProjectViewModel(model, projectManagerRepository);

            return View("EditProject", viewModel);
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new ProjectModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    projectRepository.UpdateProject(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }

        }

        // GET: ProjectController/Delete/5
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(Guid id)
        {
            var model = projectRepository.GetProjectById(id);
            var viewModel = new ProjectViewModel(model, projectManagerRepository);

            return View("DeleteProject", viewModel);
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                projectRepository.DeleteProject(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
