namespace TaskListSimulator.Shared.Repositories
{
    using System.Collections.Generic;
    using TaskListSimulator.Shared.Models;
    public interface ITaskTableRepository
    {
        void Add(TasksEntity entity);
        bool Contains(TasksEntity entityToAdd);
        TasksEntity GetTaskById(int id);
        bool ContainsById(int id);
        List<TasksEntity> GetListOfTasks();
        void SaveChanges();       
    }
}
