namespace TaskListSimulator.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using TaskListSimulator.Shared.Components;
    using TaskListSimulator.Shared.Models;
    using TaskListSimulator.Shared.Services;

    public class QueueTaskReader
    {
        private readonly IMessageQueueProvider messageQueueProvider;
        private readonly ITaskService taskService;
        public QueueTaskReader(IMessageQueueProvider messageQueueProvider, ITaskService taskService)
        {
            this.messageQueueProvider = messageQueueProvider;
            this.taskService = taskService;
        }
        public void ReadTasks()
        {
            List<TasksEntity> tasks = new List<TasksEntity>();

            try
            {
               tasks = (List<TasksEntity>)messageQueueProvider.ReceiveMessages();
            }
            catch (Exception)
            {
                Console.WriteLine($"error when working with queue");
                return;
            }
            if ((tasks.Count == 0) || (tasks == null))
                return;

            bool result = taskService.AddListOfTasks(tasks);

            if (result == true)
                Console.WriteLine($"{tasks.Count} new tasks was added to db succesfully");
            else
                Console.WriteLine("The save operation in the database is canceled");
        }
    }
}

