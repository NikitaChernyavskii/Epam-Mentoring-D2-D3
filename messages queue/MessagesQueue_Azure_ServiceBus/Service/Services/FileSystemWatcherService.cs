using System.Configuration;
using System.IO;
using Service.Contract;
using System.Text;
using Microsoft.Azure.EventHubs;

namespace Service.Services
{
    public class FileSystemWatcherService : IFileSystemWatcherService
    {
        private FileSystemWatcher _watcher;
        private EventHubClient _eventHubClient;
        private string _eventHubConnectionString = "Endpoint=sb://filecheckeventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=TkIJuV86DQvTce1h2wF8xW6Gn/Yout7s4gF6HBOzylw=";

        public FileSystemWatcherService()
        {
            _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["FolderPath"]);
            InitializeEventHub();
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
            _eventHubClient.Close();
        }

        private void OnDirectoryChange(object source, FileSystemEventArgs e)
        {
            EventData eventData = new EventData(Encoding.UTF8.GetBytes($"File name: {e.Name}. ChangeType: {e.ChangeType}"));
            _eventHubClient.SendAsync(eventData).Wait();

        }

        private void InitializeEventHub()
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(_eventHubConnectionString)
            {
                EntityPath = "FileCheckEventHubDemo"
            };

            _eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }
    }
}
