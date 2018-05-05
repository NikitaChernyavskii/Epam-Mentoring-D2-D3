using MainService.Contract;
using System;
using System.Messaging;

namespace MainService.Services
{
    public class QueueService : IQueueService
    {
        private readonly string _queueName = @".\private$\FileQueue";
        private MessageQueue _messageQueue;

        public QueueService()
        {
            InitializaQueue();
        }

        public string ReceiveMessage()
        {
            Message message = _messageQueue.Receive();
            string stringMessage = message.Body as string;

            if (String.IsNullOrEmpty(stringMessage))
            {
                throw new Exception("stringMessage is null or empty");
            }

            return stringMessage;
        }

        private void InitializaQueue()
        {
            if (!MessageQueue.Exists(_queueName))
            {
                _messageQueue = MessageQueue.Create(_queueName);
            }
            else
            {
                _messageQueue = new MessageQueue(_queueName);
            }

            _messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
        }
    }
}
