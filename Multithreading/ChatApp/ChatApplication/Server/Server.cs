using ChatApplication.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatApplication.Server
{
    public class Server
    {
        private ILoginService _loginService;
        private IReadSocketResponseService _readSocketResponseService;
        private Socket _socket;
        private IPEndPoint _localEndPoint;
        private byte[] _dataByte = new byte[1024];

        public void Start()
        {
            _readSocketResponseService = new ReadSocketResponseService();

            _loginService = new LoginService();
            IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            _localEndPoint = new IPEndPoint(ipAddress, 3000);
            _socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _socket.Bind(_localEndPoint);
                _socket.Listen(5);
                Console.WriteLine("The server is started");

                while (true)
                {
                    Console.WriteLine("W8ing a connection");
                    ProcessNewConnection();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void ProcessNewConnection()
        {
            Socket clientSocket = _socket.Accept();
            new Thread(
                    () =>
                    {
                        string loginMessage = _readSocketResponseService.GetStringMessage(clientSocket);

                        if (_loginService.IsLogin(loginMessage))
                        {
                            Login(clientSocket, loginMessage);
                        }
                        while (true)
                        {
                            StartListenConnection(clientSocket);
                        }
                    }
                ).Start();
        }

        private void Login(Socket clientSocket, string loginMessage)
        {
            _loginService.Login(clientSocket, loginMessage);
            string responseMessage = $"User {_loginService.GetClientName(loginMessage)} has joined the chat {_readSocketResponseService.EofMarker}";
            byte[] msg = Encoding.ASCII.GetBytes(responseMessage);
            foreach (var clientInfo in _loginService.Clients)
            {
                clientInfo.Socket.Send(msg);
            }
        }

        private void StartListenConnection(Socket clientSocket)
        {
            byte[] bytes;
            while (true)
            {
                bytes = new byte[1024];
                string message = "";
                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = clientSocket.Receive(bytes);
                    message += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (message.IndexOf(_readSocketResponseService.EofMarker) > -1)
                    {
                        break;
                    }
                }

                byte[] msg = Encoding.ASCII.GetBytes(message);
                foreach (var clientInfo in _loginService.Clients)
                {
                    clientInfo.Socket.Send(msg);
                }
            }
        }
    }
}
