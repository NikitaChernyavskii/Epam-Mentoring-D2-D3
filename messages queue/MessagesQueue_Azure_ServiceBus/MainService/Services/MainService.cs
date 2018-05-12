using MainService.Services;
using System.ServiceProcess;

namespace MainService
{
    public partial class MainService : ServiceBase
    {
        private EventHubService _eventHubService;

        public MainService()
        {
            InitializeComponent();
            _eventHubService = new EventHubService();
        }

        protected override void OnStart(string[] args)
        {
            while (true)
            {
                _eventHubService.Receive().Wait();
            }
        }
    }
}
