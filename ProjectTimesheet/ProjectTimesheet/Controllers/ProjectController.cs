using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimesheet.Data;
using ProjectTimesheet.Models;
using ProjectTimesheet.Repositories;

namespace ProjectTimesheet.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectRepository projectRepository;
        public ProjectController(ApplicationDbContext dbcontext)
        {
            projectRepository = new ProjectRepository(dbcontext);
        }

        // GET: ProjectController
        public ActionResult Index()
        {
            var list = projectRepository.GetAllProjects();
            return View(list);
        }

        // GET: ProjectController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = projectRepository.GetProjectById(id);
            return View("DetailsProject", model);
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            return View("CreateProject");
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
            var model = projectRepository.GetProjectById(id);
            return View("EditProject", model);
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
        public ActionResult Delete(Guid id)
        {
            var model = projectRepository.GetProjectById(id);
            return View("DeleteProject", model);
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
