using ProjectTimesheet.Data;
using ProjectTimesheet.Models.DBObjects;
using ProjectTimesheet.Models;

namespace ProjectTimesheet.Repositories
{
    public class ProjectRepository
    {
        private readonly ApplicationDbContext _DBContext;

        public ProjectRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public ProjectRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }

        private ProjectModel MapDBObjectToModel(Project dbobject)
        {
            var model = new ProjectModel();
            if (dbobject != null)
            {
                model.IdProject = dbobject.IdProject;
                model.Name = dbobject.Name;
                model.Description = dbobject.Description;
                model.StartDate = dbobject.StartDate;
                model.EndDate = dbobject.EndDate;
                model.IdPm = dbobject.IdPm;
            }
            return model;
        }

        private Project MapModelToDBObject(ProjectModel model)
        {
            var dbobject = new Project();
            if (model != null)
            {
                dbobject.IdProject = model.IdProject;
                dbobject.Name = model.Name;
                dbobject.Description = model.Description;
                dbobject.StartDate = model.StartDate;
                dbobject.EndDate = model.EndDate;
                dbobject.IdPm = model.IdPm;
            }
            return dbobject;
        }

        public List<ProjectModel> GetAllProjects()
        {
            var list = new List<ProjectModel>();
            foreach (var dbobject in _DBContext.Projects)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public ProjectModel GetProjectById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Projects.FirstOrDefault(x => x.IdProject == id));
        }
        public void InsertProject(ProjectModel model)
        {
            model.IdProject = Guid.NewGuid();
            _DBContext.Projects.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateProject(ProjectModel model)
        {
            var dbobject = _DBContext.Projects.FirstOrDefault(x => x.IdProject == model.IdProject);
            if (dbobject != null)
            {
                dbobject.IdProject = model.IdProject;
                dbobject.Name = model.Name;
                dbobject.Description = model.Description;
                dbobject.StartDate = model.StartDate;
                dbobject.EndDate = model.EndDate;
                dbobject.IdPm = model.IdPm;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteProject(Guid id)
        {
            var dbobject = _DBContext.Projects.FirstOrDefault(x => x.IdProject == id);
            if (dbobject != null)
            {
                _DBContext.Projects.Remove(dbobject);
                _DBContext.SaveChanges();
            }

        }
    }
}
