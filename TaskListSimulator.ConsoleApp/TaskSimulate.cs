namespace TaskListSimulator.ConsoleApp
{ 
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using TaskListSimulator.ConsoleApp.Components;
    using TaskListSimulator.Shared.Models;
    public class TaskSimulate
    {
        public void Run()
        {
            while (true)
            {
                Console.WriteLine(@"
-----------------
Use one of the option:
    1-Show list of tasks
    2-Add task to queue
-----------------");
                string inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "1":
                        this.ShowTasks();
                        break;
                    case "2":
                        this.AddTaskToQueue();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ShowTasks()
        {
            try 
            { 
                var stringResult = Web.Get($"https://localhost:44304/api/tasks");
                string jsonResult = stringResult.Replace(@"\", string.Empty);
                string jsonForDeserialize = jsonResult.Trim().Substring(1, (jsonResult.Length) - 2);
                var tasksEntities = JsonConvert.DeserializeObject<List<TasksEntity>>(jsonForDeserialize);
                foreach (var item in tasksEntities)
                    Console.WriteLine(item.TaskId + " " + item.Description + " " + item.CreateTime);

            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            };

        }

        private void AddTaskToQueue()
        {
            Console.WriteLine("Please input task description:");
            string task = Console.ReadLine();

            try 
            { 
                Console.WriteLine(Web.Post($"https://localhost:44304/api/tasks", task)); 
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
            };
        }
    }
}
