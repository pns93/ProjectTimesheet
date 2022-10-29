using ProjectTimesheet.Data;
using ProjectTimesheet.Models.DBObjects;
using ProjectTimesheet.Models;

namespace ProjectTimesheet.Repositories
{
    public class ProjectManagerRepository
    {
        private readonly ApplicationDbContext _DBContext;

        public ProjectManagerRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public ProjectManagerRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }

        private ProjectManagerModel MapDBObjectToModel(ProjectManager dbobject)
        {
            var model = new ProjectManagerModel();
            if (dbobject != null)
            {
                model.IdPm = dbobject.IdPm;
                model.Name = dbobject.Name;
                model.Department = dbobject.Department;

            }
            return model;
        }

        private ProjectManager MapModelToDBObject(ProjectManagerModel model)
        {
            var dbobject = new ProjectManager();
            if (model != null)
            {
                dbobject.IdPm = model.IdPm;
                dbobject.Name = model.Name;
                dbobject.Department = model.Department;
            }
            return dbobject;
        }

        public List<ProjectManagerModel> GetAllProjectManagers()
        {
            var list = new List<ProjectManagerModel>();
            foreach (var dbobject in _DBContext.ProjectManagers)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public ProjectManagerModel GetProjectManagerById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.ProjectManagers.FirstOrDefault(x => x.IdPm == id));
        }
        public void InsertProjectManager(ProjectManagerModel model)
        {
            model.IdPm = Guid.NewGuid();
            _DBContext.ProjectManagers.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateProjectManager(ProjectManagerModel model)
        {
            var dbobject = _DBContext.ProjectManagers.FirstOrDefault(x => x.IdPm == model.IdPm);
            if (dbobject != null)
            {
                dbobject.IdPm = model.IdPm;
                dbobject.Name = model.Name;
                dbobject.Department = model.Department;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteProjectManager(Guid id)
        {
            var dbobject = _DBContext.ProjectManagers.FirstOrDefault(x => x.IdPm == id);
            if (dbobject != null)
            {
                _DBContext.ProjectManagers.Remove(dbobject);
                _DBContext.SaveChanges();
            }

        }
    }
}
