using ChatApplication.Models;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatApplication.Server
{
    public class LoginService : ILoginService
    {
        public List<ClientInfo> Clients { get; private set; }
        private const string _loginMarker = "login";

        public LoginService()
        {
            Clients = new List<ClientInfo>();
        }

        public bool IsLogin(string message)
        {
            return message.Trim().ToLower().StartsWith(_loginMarker);
        }

        public string GetClientName(string message)
        {
            return message.Trim().ToLower().Substring(_loginMarker.Length);
        }

        public void Login(Socket clientSocket, string message)
        {
            ClientInfo clientInfo = new ClientInfo
            {
                Socket = clientSocket,
                Name = GetClientName(message)
            };

            Clients.Add(clientInfo);
        }
    }
}
