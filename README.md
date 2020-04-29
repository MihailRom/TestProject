# TestProject

WebApi:
  Controllers in TaskListSimulator.WebApi/Controllers/TasksController.cs
  Dependency injection in TaskListSimulator.WebApi/App_start/NinjectWebCommon.cs -> RegisterServices(IKernel kernel) method
  
ConsoleApp:
  Dependency injection in TaskListSimulator.ConsoleApp/Dependencies/TaskListSimulateRegistry.cs
  TaskSimulate.cs for Get and Post queries
  QueueTaskReader.cs for read queue and add tasks to db
  
Shared:
  Components for work with MSMQ and Database (Entity Framework)

