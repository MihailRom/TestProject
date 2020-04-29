namespace TaskListSimulator.ConsoleApp
{
    using StructureMap;
    using System.Timers;
    using TaskListSimulator.ConsoleApp.Components;
    using TaskListSimulator.ConsoleApp.Dependencies;

    class Program
    {
        static void Main(string[] args)
        {

            var container = new Container(new TaskListSimulateRegistry());
            var queueReadTask = container.GetInstance<QueueTaskReader>();
            var taskSimulator = container.GetInstance<TaskSimulate>();

            Timer operationTimer = new Timer(10000) { AutoReset = true };
            operationTimer.Elapsed += (sender, e) => { queueReadTask.ReadTasks(); };
            operationTimer.Enabled = true;

            taskSimulator.Run();
            
        }
    }
}
