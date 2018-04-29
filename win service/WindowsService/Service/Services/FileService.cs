using System.ServiceProcess;
using Service.Contract;

namespace Service.Services
{
    partial class FileService : ServiceBase
    {
        private readonly IFileSystemWatcherService _fileSystemWatcherService;

        public FileService()
        {
            _fileSystemWatcherService = new FileSystemWatcherService();
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _fileSystemWatcherService.WatchFolder();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
    }
}
