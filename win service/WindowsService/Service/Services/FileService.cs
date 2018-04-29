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
    }
}
