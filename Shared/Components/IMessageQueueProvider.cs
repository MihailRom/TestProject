namespace TaskListSimulator.Shared.Components
{
    using System;
    using System.Collections.Generic;
    using TaskListSimulator.Shared.Models;

    public interface IMessageQueueProvider : IDisposable
    {
        void SendMessage(TasksEntity taskEntity);
        TasksEntity ReceiveMessage();
        IEnumerable<TasksEntity> ReceiveMessages();
    }
}
