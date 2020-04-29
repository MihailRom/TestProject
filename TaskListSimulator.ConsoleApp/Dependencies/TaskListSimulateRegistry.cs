using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskListSimulator.Shared.Components;
using TaskListSimulator.Shared.Models;
using TaskListSimulator.Shared.Repositories;
using TaskListSimulator.Shared.Services;

namespace TaskListSimulator.ConsoleApp.Dependencies
{
    class TaskListSimulateRegistry : Registry
    {
        public TaskListSimulateRegistry()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionStringToDB"].ConnectionString;
            this.For<TaskListSimulatorDBContext>().Use<TaskListSimulatorDBContext>().Ctor<string>("connectionString").Is(connectionString);
            this.For<IMessageQueueProvider>().Use<MSMsgQueue>();
            this.For<ITaskTableRepository>().Use<TaskTableRepository>();
            this.For<ITaskService>().Use<TaskService>();
            this.For<ITaskService>().Use<TaskService>();
        }
    }
}
