using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimesheet.Data;
using ProjectTimesheet.Models;
using ProjectTimesheet.Repositories;
using System.Data;

namespace ProjectTimesheet.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ProjectManagerController : Controller
    {
        private ProjectManagerRepository projectManagerRepository;
        public ProjectManagerController(ApplicationDbContext dbcontext)
        {
            projectManagerRepository = new ProjectManagerRepository(dbcontext);

        }
        // GET: ProjectManager
        public ActionResult Index()
        {
            var list = projectManagerRepository.GetAllProjectManagers();
            return View(list);
        }
        // GET: ProjectManager/Details/5
        public ActionResult Details(Guid id)
        {
            var model = projectManagerRepository.GetProjectManagerById(id);
            return View("DetailsProjectManager", model);
        }

        // GET: ProjectManager/Create
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            return View("CreateProjectManager");
        }

        // POST: ProjectManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ProjectManagerModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    projectManagerRepository.InsertProjectManager(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateProjectManager");
            }
        }

        // GET: ProjectManager/Edit/5
        [Authorize(Roles = "Admin")]

        public ActionResult Edit(Guid id)
        {
            var model = projectManagerRepository.GetProjectManagerById(id);
            return View("EditProjectManager", model);
        }

        // POST: ProjectManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new ProjectManagerModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    projectManagerRepository.UpdateProjectManager(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }

        }


        // GET: ProjectManager/Delete/5
        [Authorize(Roles = "Admin")]

        public ActionResult Delete(Guid id)
        {
            var model = projectManagerRepository.GetProjectManagerById(id);
            return View("DeleteProjectManager",model);
        }

        // POST: ProjectManager/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                projectManagerRepository.DeleteProjectManager(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
