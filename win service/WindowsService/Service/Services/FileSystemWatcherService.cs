
using System.Configuration;
using System.IO;

namespace Service.Services
{
    internal class FileSystemWatcherService
    {
        public void WatchFolder()
        {
            var watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["FolderPath"]);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += new FileSystemEventHandler(OnDirectoryChange);
            watcher.EnableRaisingEvents = true;
        }

        private void OnDirectoryChange(object source, FileSystemEventArgs e)
        {

        }
    }
}
