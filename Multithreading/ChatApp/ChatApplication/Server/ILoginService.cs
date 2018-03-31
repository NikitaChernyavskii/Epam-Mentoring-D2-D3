using ChatApplication.Models;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatApplication.Server
{
    public interface ILoginService
    {
        List<ClientInfo> Clients { get; }

        bool IsLogin(string message);

        string GetClientName(string message);

        void Login(Socket clientSocket, string message);
    }
}
