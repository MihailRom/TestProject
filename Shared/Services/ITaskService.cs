namespace TaskListSimulator.Shared.Services
{
    using System.Collections.Generic;
    using TaskListSimulator.Shared.Models;
    public interface ITaskService
    {
        int AddNewTask(TasksEntity task);
        bool AddListOfTasks(List<TasksEntity> tasksEntities);
        List<TasksEntity> GetListOfTasks();
        TasksEntity GetTaskById(int id);
    }
}