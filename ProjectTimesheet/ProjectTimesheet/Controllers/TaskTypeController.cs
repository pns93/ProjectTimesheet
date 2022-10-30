using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectTimesheet.Data;
using ProjectTimesheet.Models;
using ProjectTimesheet.Repositories;

namespace ProjectTimesheet.Controllers
{
    public class TaskTypeController : Controller
    {

        private TaskTypeRepository taskTypeRepository;
        public TaskTypeController(ApplicationDbContext dbcontext)
        {
            taskTypeRepository = new TaskTypeRepository(dbcontext);
        }
        // GET: TaskTypeController
        public ActionResult Index()
        {
            var list = taskTypeRepository.GetAllTaskTypes();
            return View(list);
        }

        // GET: TaskTypeController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = taskTypeRepository.GetTaskTypeById(id);
            return View("DetailsTaskType", model);
        }

        // GET: TaskTypeController/Create
        public ActionResult Create()
        {
            return View("CreateTaskType");
        }

        // POST: TaskTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new TaskTypeModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    taskTypeRepository.InsertTaskType(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateTaskType");
            }
        }

        // GET: TaskTypeController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = taskTypeRepository.GetTaskTypeById(id);
            return View("EditTaskType", model);
        }

        // POST: TaskTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new TaskTypeModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    taskTypeRepository.UpdateTaskType(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit", id);
            }

        }

        // GET: TaskTypeController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = taskTypeRepository.GetTaskTypeById(id);
            return View("DeleteTaskType", model);
        }

        // POST: TaskTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                taskTypeRepository.DeleteTaskType(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
