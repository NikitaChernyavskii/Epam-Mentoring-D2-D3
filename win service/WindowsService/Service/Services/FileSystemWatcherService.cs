
using System;
using System.Configuration;
using System.IO;
using Service.Contract;

namespace Service.Services
{
    public class FileSystemWatcherService : IFileSystemWatcherService
    {
        private readonly string _logFile;
        private FileSystemWatcher _watcher;

        public FileSystemWatcherService()
        {
            _logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileService.log");
            _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["FolderPath"]);
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
            File.AppendAllText(_logFile, $"File name: {e.Name}. ChangeType: {e.ChangeType}");

            // emulate crashing
            if(e.Name == "Crash.txt")
            {
                throw new Exception();
            }
    }
    }
}
