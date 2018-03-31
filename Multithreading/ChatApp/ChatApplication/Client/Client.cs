using ChatApplication.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatApplication.Client
{
    public class Client
    {
        private IReadSocketResponseService _readSocketResponseService;
        private const int _port = 3000;
        private IPHostEntry _ipHostInfo;
        private IPAddress _ipAddress;
        private IPEndPoint _remoteEp;
        private Socket _clientSocket;

        private string _userName;

        public void Start(string userName)
        {
            _readSocketResponseService = new ReadSocketResponseService();

            _ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            _ipAddress = _ipHostInfo.AddressList[0];
            _remoteEp = new IPEndPoint(_ipAddress, _port);
            _clientSocket = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _userName = userName;
            _clientSocket.Connect(_remoteEp);
            SendMessage("login" + userName);
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] messageBytes = Encoding.ASCII.GetBytes($"{message}{_readSocketResponseService.EofMarker}");

                _clientSocket.Send(messageBytes);
                var result = _readSocketResponseService.GetStringMessage(_clientSocket);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
