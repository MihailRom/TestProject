namespace TaskListSimulator.Shared.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TaskListSimulator.Shared.Models;
    public class TaskTableRepository : ITaskTableRepository
    {
        private readonly TaskListSimulatorDBContext dbContext;

        public TaskTableRepository(TaskListSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(TasksEntity entity)
        {
            this.dbContext.Tasks.Add(entity);
        }

        public bool Contains(TasksEntity entityToAdd)
        {
            return this.dbContext.Tasks.Any(t => t.Description == entityToAdd.Description);
        }

        public bool ContainsById(int id)
        {
            return this.dbContext.Tasks.Any(t => t.TaskId == id);
        }
        public List<TasksEntity> GetListOfTasks()
        {
            List<TasksEntity> listItems = new List<TasksEntity>();
            foreach (var item in this.dbContext.Tasks)
            {
                listItems.Add(item);
            }
            return listItems;
        }

        public TasksEntity GetTaskById(int id)
        {
            var resultTask = new TasksEntity();

            return this.dbContext.Tasks.First(t => t.TaskId == id);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
