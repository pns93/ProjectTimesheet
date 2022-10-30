using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimesheet.Data;
using ProjectTimesheet.Models;
using ProjectTimesheet.Repositories;

namespace ProjectTimesheet.Controllers
{
    public class ProjectTaskController : Controller
    {
        private ProjectTaskRepository projectTaskRepository;
        public ProjectTaskController(ApplicationDbContext dbcontext)
        {
            projectTaskRepository = new ProjectTaskRepository(dbcontext);
        }
        // GET: ProjectTaskController
        public ActionResult Index()
        {
            var list = projectTaskRepository.GetAllProjectTasks();
            return View(list);
        }

        // GET: ProjectTaskController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = projectTaskRepository.GetProjectTaskById(id);
            return View("DetailsProjectTask", model);
        }

        // GET: ProjectTaskController/Create
        public ActionResult Create()
        {
            return View("CreateProjectTask");
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
            var model = projectTaskRepository.GetProjectTaskById(id);
            return View("EditProjectTask", model);
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
                return RedirectToAction("Edit", id);
            }

        }

        // GET: ProjectTaskController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = projectTaskRepository.GetProjectTaskById(id);
            return View("DeleteProjectTask", model);
        }

        // POST: ProjectTaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
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
