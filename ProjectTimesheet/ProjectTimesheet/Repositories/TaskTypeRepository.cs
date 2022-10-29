using ProjectTimesheet.Data;
using ProjectTimesheet.Models.DBObjects;
using ProjectTimesheet.Models;

namespace ProjectTimesheet.Repositories
{
    public class TaskTypeRepository
    {
        private readonly ApplicationDbContext _DBContext;

        public TaskTypeRepository()
        {
            _DBContext = new ApplicationDbContext();
        }
        public TaskTypeRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }

        private TaskTypeModel MapDBObjectToModel(TaskType dbobject)
        {
            var model = new TaskTypeModel();
            if (dbobject != null)
            {
                model.IdTaskType = dbobject.IdTaskType;
                model.Name = dbobject.Name;
                model.Description = dbobject.Description; ;

            }
            return model;
        }

        private TaskType MapModelToDBObject(TaskTypeModel model)
        {
            var dbobject = new TaskType();
            if (model != null)
            {
                dbobject.IdTaskType = model.IdTaskType;
                dbobject.Name = model.Name;
                dbobject.Description = model.Description;
            }
            return dbobject;
        }

        public List<TaskTypeModel> GetAllTaskTypes()
        {
            var list = new List<TaskTypeModel>();
            foreach (var dbobject in _DBContext.TaskTypes)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public TaskTypeModel GetTaskTypeById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.TaskTypes.FirstOrDefault(x => x.IdTaskType == id));
        }
        public void InsertTaskType(TaskTypeModel model)
        {
            model.IdTaskType = Guid.NewGuid();
            _DBContext.TaskTypes.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateTaskType(TaskTypeModel model)
        {
            var dbobject = _DBContext.TaskTypes.FirstOrDefault(x => x.IdTaskType == model.IdTaskType);
            if (dbobject != null)
            {
                dbobject.IdTaskType = model.IdTaskType;
                dbobject.Name = model.Name;
                dbobject.Description = model.Description;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteTaskType(Guid id)
        {
            var dbobject = _DBContext.TaskTypes.FirstOrDefault(x => x.IdTaskType == id);
            if (dbobject != null)
            {
                _DBContext.TaskTypes.Remove(dbobject);
                _DBContext.SaveChanges();
            }

        }
    }
}
