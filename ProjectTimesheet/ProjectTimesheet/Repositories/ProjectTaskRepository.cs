using ProjectTimesheet.Data;
using ProjectTimesheet.Models;
using ProjectTimesheet.Models.DBObjects;
using ProjectTimesheet.ViewModel;

namespace ProjectTimesheet.Repositories
{
    public class ProjectTaskRepository
    {
        private readonly ApplicationDbContext _DBContext;

        public ProjectTaskRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public ProjectTaskRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }

        private ProjectTaskModel MapDBObjectToModel(ProjectTask dbobject)
        {
            var model = new ProjectTaskModel();
            if (dbobject != null)
            {
                model.IdTask = dbobject.IdTask;
                model.IdEmployee = dbobject.IdEmployee;
                model.Name = dbobject.Name;
                model.Description = dbobject.Description;
                model.StartDate = dbobject.StartDate;
                model.EndDate = dbobject.EndDate;
                model.IdProject = dbobject.IdProject;
                model.IsApproved = dbobject.IsApproved;
                model.IdTaskType = dbobject.IdTaskType;
                model.IdTaskType = dbobject.IdTaskType;

            }
            return model;
        }

        private ProjectTask MapModelToDBObject(ProjectTaskModel model)
        {
            var dbobject = new ProjectTask();
            if (model != null)
            {
                dbobject.IdTask = model.IdTask;
                dbobject.IdEmployee = model.IdEmployee;
                dbobject.Name = model.Name;
                dbobject.Description = model.Description;
                dbobject.StartDate = model.StartDate;
                dbobject.EndDate = model.EndDate;
                dbobject.IdProject = model.IdProject;
                dbobject.IsApproved = model.IsApproved;
                dbobject.IdTaskType = model.IdTaskType;
            }
            return dbobject;
        }

        public List<ProjectTaskModel> GetAllProjectTasks()
        {
            var list = new List<ProjectTaskModel>();
            foreach (var dbobject in _DBContext.ProjectTasks)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public ProjectTaskModel GetProjectTaskById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.ProjectTasks.FirstOrDefault(x => x.IdTask == id));
        }
        public void InsertProjectTask(ProjectTaskModel model)
        {
            model.IdTask = Guid.NewGuid();
            _DBContext.ProjectTasks.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateProjectTask(ProjectTaskModel model)
        {
            var dbobject = _DBContext.ProjectTasks.FirstOrDefault(x => x.IdTask == model.IdTask);
            if (dbobject != null)
            {
                dbobject.IdTask = model.IdTask;
                dbobject.IdEmployee = model.IdEmployee;
                dbobject.Name = model.Name;
                dbobject.Description = model.Description;
                dbobject.StartDate = model.StartDate;
                dbobject.EndDate = model.EndDate;
                dbobject.IdProject = model.IdProject;
                dbobject.IsApproved = model.IsApproved;
                dbobject.IdTaskType = model.IdTaskType;

                _DBContext.SaveChanges();
            }
        }
        public void DeleteProjectTask(Guid id)
        {
            var dbobject = _DBContext.ProjectTasks.FirstOrDefault(x => x.IdTask == id);
            if (dbobject != null)
            {
                _DBContext.ProjectTasks.Remove(dbobject);
                _DBContext.SaveChanges();
            }

        }
    }
}
