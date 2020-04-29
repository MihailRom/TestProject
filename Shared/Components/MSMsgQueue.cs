namespace TaskListSimulator.Shared.Components
{
    using System;
    using System.Collections.Generic;
    using System.Messaging;
    using TaskListSimulator.Shared.Models;

    public class MSMsgQueue : IMessageQueueProvider
    {
        private bool isInitialized = false;
        private readonly string queueName = @".\private$\Test";
        private MessageQueue messageQueue;
        public MSMsgQueue()
        {

        }

        private void InitializeQueue()
        {
            if (!MessageQueue.Exists(queueName))
            {
                try
                {
                    MessageQueue.Create(queueName);
                }
                catch (MessageQueueException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            messageQueue = new MessageQueue(queueName);
            this.isInitialized = true;
        }

        public void SendMessage(TasksEntity taskEntity)
        {
            if (!this.isInitialized)
                this.InitializeQueue();

            messageQueue.Send(taskEntity);
        }

        public TasksEntity ReceiveMessage()
        {
            if (!this.isInitialized)
                this.InitializeQueue();

            TasksEntity taskEntity = new TasksEntity();
            try
            {
                messageQueue.Formatter = new XmlMessageFormatter
                             (new Type[] { typeof(TasksEntity) });
                Message myMessage = messageQueue.Receive(new TimeSpan(0, 0, 2)); 
                taskEntity = (TasksEntity)myMessage.Body;
            }

            catch (MessageQueueException msgEx)
            {
                if (msgEx.MessageQueueErrorCode != MessageQueueErrorCode.IOTimeout)
                    Console.WriteLine(msgEx.Message);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            return taskEntity;

        }

        public IEnumerable<TasksEntity> ReceiveMessages()
        {
            if (!this.isInitialized)
                this.InitializeQueue();

            List<TasksEntity> lstTasks = new List<TasksEntity>();
            bool continueToSeekForMessages = true;

            while (continueToSeekForMessages)
            {
                try
                {
                    messageQueue.Formatter = new XmlMessageFormatter
                                (new Type[] { typeof(TasksEntity) });
                    var message = messageQueue.Receive(new TimeSpan(0, 0, 2));
                    var messageBody = (TasksEntity)message.Body;
                    lstTasks.Add(messageBody);
                }
                catch (MessageQueueException msgEx)
                {
                    if (msgEx.MessageQueueErrorCode != MessageQueueErrorCode.IOTimeout)
                        Console.WriteLine(msgEx.Message);
                    continueToSeekForMessages = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continueToSeekForMessages = false;
                }
            }
            return lstTasks;
        }
        public void Dispose()
        {
            messageQueue.Dispose();
        }
    }
}
