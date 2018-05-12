using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace MainService.Services
{
    public class EventHubService
    {
        private string _eventHubPath = "FileCheckEventHubDemo";
        private string _eventHubConnectionString = "Endpoint=sb://filecheckeventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=TkIJuV86DQvTce1h2wF8xW6Gn/Yout7s4gF6HBOzylw=";
        private string _storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=filecheckstorage;AccountKey=R6zGA3k8dxSzDgeBTmMLqtXcTovUN+grUC6wCd3j/b5je731owjKhJ8TT0juFcAb8KIFZfaC7n0Z1Mk7ynFepA==;EndpointSuffix=core.windows.net";
        private string _leaseContainerName = "filecheckstorage";

        public async Task Receive()
        {
            var eventProcessorHost = new EventProcessorHost(
                _eventHubPath,
                PartitionReceiver.DefaultConsumerGroupName,
                _eventHubConnectionString,
                _storageConnectionString,
                _leaseContainerName);

            await eventProcessorHost.RegisterEventProcessorAsync<FileCheckEventHubProcessor>();
        }
    }
}
