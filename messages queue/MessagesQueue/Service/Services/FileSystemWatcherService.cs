using System;
using System.Configuration;
using System.IO;
using Service.Contract;
using System.Messaging;

namespace Service.Services
{
    public class FileSystemWatcherService : IFileSystemWatcherService
    {
        private readonly string _queueName = @".\private$\FileQueue";
        private FileSystemWatcher _watcher;
        private MessageQueue _messageQueue;

        public FileSystemWatcherService()
        {
            _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["FolderPath"]);
            InitializaQueue();
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
        }

        private void OnDirectoryChange(object source, FileSystemEventArgs e)
        {
            string message = $"File name: {e.Name}. ChangeType: {e.ChangeType}";
            _messageQueue.Send(message);
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
