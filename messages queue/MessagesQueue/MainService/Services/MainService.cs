using MainService.Contract;
using MainService.Services;
using System;
using System.IO;
using System.ServiceProcess;

namespace MainService
{
    public partial class MainService : ServiceBase
    {
        private readonly IQueueService _queueService;
        private readonly string _logFile;

        public MainService()
        {
            InitializeComponent();
            _queueService = new QueueService();
            _logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileService.log");
        }

        protected override void OnStart(string[] args)
        {
            while (true)
            {
                string message = _queueService.ReceiveMessage();
                File.AppendAllText(_logFile, message);
            }
        }
    }
}
