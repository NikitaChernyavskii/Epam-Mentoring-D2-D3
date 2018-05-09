using System;
using System.Configuration;
using System.IO;
using Service.Contract;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;

namespace Service.Services
{
    public class FileSystemWatcherService : IFileSystemWatcherService
    {
        const string queueName = "CheckFilesQueue";
        private QueueClient _queueClient;
        private FileSystemWatcher _watcher;

        public FileSystemWatcherService()
        {
            _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["FolderPath"]);
            InitializeQueue();
            _queueClient = QueueClient.Create(queueName);
        }

        public void WatchFolder()
        {
            _watcher.NotifyFilter = NotifyFilters.LastWrite;
            _watcher.Changed += OnDirectoryChange;
            _watcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            _watcher.Dispose();
            _queueClient.Close();
        }

        private void OnDirectoryChange(object source, FileSystemEventArgs e)
        {
            var message = new BrokeredMessage($"File name: {e.Name}. ChangeType: {e.ChangeType}");
            _queueClient.Send(message);
        }

        private void InitializeQueue()
        {
            NamespaceManager namespaceManager = NamespaceManager.Create();

            if (namespaceManager.QueueExists(queueName))
            {
                namespaceManager.DeleteQueue(queueName);
            }

            namespaceManager.CreateQueue(queueName);
        }
    }
}
