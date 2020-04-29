namespace TaskListSimulator.Shared.Models
{
    using System;
    public class TasksEntity
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
    }
}