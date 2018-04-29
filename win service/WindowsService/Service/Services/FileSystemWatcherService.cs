
using System;
using System.Configuration;
using System.IO;
using Service.Contract;

namespace Service.Services
{
    public class FileSystemWatcherService : IFileSystemWatcherService
    {
        private readonly string _logFile;

        public FileSystemWatcherService()
        {
            _logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileService.log");
        }

        public void WatchFolder()
        {
            var watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["FolderPath"]);
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += OnDirectoryChange;
            watcher.EnableRaisingEvents = true;
        }

        private void OnDirectoryChange(object source, FileSystemEventArgs e)
        {
            File.AppendAllText(_logFile, $"File name: {e.Name}. ChangeType: {e.ChangeType}");
        }
    }
}
