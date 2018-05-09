using MainService.Contract;
using Microsoft.ServiceBus.Messaging;
using System;

namespace MainService.Services
{
    public class QueueService : IQueueService
    {
        const string queueName = "CheckFilesQueue";
        private QueueClient _queueClient;

        public QueueService()
        {
            _queueClient = QueueClient.Create(queueName);
        }

        public string ReceiveMessage()
        {
            var queueClient = QueueClient.Create(queueName, ReceiveMode.ReceiveAndDelete);
            var message = _queueClient.Receive();
            var stringMessage = message.GetBody<string>();

            if (String.IsNullOrEmpty(stringMessage))
            {
                throw new Exception("stringMessage is null or empty");
            }

            return stringMessage;
        }
    }
}
