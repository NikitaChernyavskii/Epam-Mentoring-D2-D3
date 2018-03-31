using System.Net.Sockets;

namespace ChatApplication.Models
{
    public class ClientInfo
    {
        public Socket Socket { get; set; }

        public string Name { get; set; }
    }
}
