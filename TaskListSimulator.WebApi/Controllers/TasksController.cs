namespace WebCore.Controllers
{
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using TaskListSimulator.Shared.Components;
    using TaskListSimulator.Shared.Models;
    using TaskListSimulator.Shared.Services;
   public class TasksController : ApiController
   {
       private readonly ITaskService tasksService;
       private readonly IMessageQueueProvider messageQueueProvider;
       public TasksController(ITaskService tasksService, IMessageQueueProvider messageQueueProvider)
       {
            this.tasksService = tasksService;
            this.messageQueueProvider = messageQueueProvider;
       }
       // Get api/tasks
       public string Get()
       {
           return JsonConvert.SerializeObject(tasksService.GetListOfTasks());
       }
        // Post api/tasks
        public HttpResponseMessage Post([FromBody]string description)
        {
            if (description.Replace(" ", "") == String.Empty)
                return Request.CreateResponse(HttpStatusCode.NoContent, "WebApi: Task has no description");

            TasksEntity newTask = new TasksEntity
            {
                Description = description,
                CreateTime = DateTime.Now
            };

            try
            {
                 messageQueueProvider.SendMessage(newTask);
            }
            catch (Exception ex)
            {
                HttpResponseMessage badResponse = Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                return badResponse;
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, $"WebApi: Task {newTask.Description} {newTask.CreateTime} added to queue succesfully");
            return response;
        }
    }
}
