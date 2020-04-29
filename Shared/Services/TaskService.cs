namespace TaskListSimulator.Shared.Services
{
    using System;
    using System.Collections.Generic;
    using TaskListSimulator.Shared.Repositories;
    using TaskListSimulator.Shared.Models;
    using System.Transactions;

    public class TaskService : ITaskService
    {
        private readonly ITaskTableRepository taskTableRepository;
        public TaskService(ITaskTableRepository taskTableRepository)
        {
            this.taskTableRepository = taskTableRepository;
        }
        public int AddNewTask(TasksEntity task)
        {
            var entityToAdd = new TasksEntity()
            {
                Description = task.Description,
                CreateTime = task.CreateTime
            };

            if (taskTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException($"Attention. This task \"{entityToAdd.Description}\" has been registered");
            }

            taskTableRepository.Add(entityToAdd);

            taskTableRepository.SaveChanges();

            return entityToAdd.TaskId;
        }

        public List<TasksEntity> GetListOfTasks()
        {
            return taskTableRepository.GetListOfTasks();
        }

        public TasksEntity GetTaskById(int id)
        {
            if (!taskTableRepository.ContainsById(id))
            {
                throw new ArgumentException($"Task with id {id} hasn`t been registered.");
            }
            return taskTableRepository.GetTaskById(id);
        }

        public bool AddListOfTasks(List<TasksEntity> listOfTaskEntities)
        {
            bool returnValue = false;

            if ((listOfTaskEntities.Count == 0) || (listOfTaskEntities == null))
            {
                Console.WriteLine("There are no objects to write to db");
                return returnValue;
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach(var item in listOfTaskEntities)
                    {
                        this.AddNewTask(item);
                    }                    
                    scope.Complete();
                    returnValue = true;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Exception message: {ex.Message}");
            }
            catch (TransactionAbortedException ex)
            {
                Console.WriteLine($"Transaction aborted exception message: {ex.Message}");
            }
            return returnValue;
        }
    }
}
