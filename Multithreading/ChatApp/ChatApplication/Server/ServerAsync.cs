using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ChatApplication.Models;

namespace ChatApplication.Server
{
    public class ServerAsync
    {
        private List<ClientInfo> _clients;
        private byte[] _dataByte = new byte[1024];
        private const string _loginMarker = "login";
        private Socket _serverSocket;

        public ServerAsync()
        {
            //_clients = new ConcurrentDictionary<int, ClientAsync>();
            _clients = new List<ClientInfo>();
        }

        public void Start()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 3000);
            _serverSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                _serverSocket.Bind(localEndPoint);
                _serverSocket.Listen(10);

                Console.WriteLine("Waiting for a connection...");
                _serverSocket.BeginAccept(AcceptCallback, _serverSocket);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket clientSocket = listener.EndAccept(ar);
            _serverSocket.BeginAccept(AcceptCallback, _serverSocket);


            clientSocket.BeginReceive(_dataByte, 0, _dataByte.Length, 0, EndReceiveCallback, clientSocket);
        }

        private void EndReceiveCallback(IAsyncResult ar)
        {
            Socket clientSocket = (Socket)ar.AsyncState;
            string inputMessage = ReadMessage(_dataByte);
            if (IsLogin(inputMessage))
            {
                Login(clientSocket, inputMessage);
                Send($"{GetClientName(inputMessage)}");

                return;
            }


        }

        //private void Send(Socket clientSocker, string message)
        private void Send(string message)
        {
            byte[] byteData = Encoding.Default.GetBytes(message);
            foreach (var clientInfo in _clients)
            {
                clientInfo.Socket.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, clientInfo.Socket);
            }
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                int bytesSent = clientSocket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private string ReadMessage(byte[] data)
        {
            return Encoding.Default.GetString(data);
        }

        private bool IsLogin(string message)
        {
            return message.Trim().ToLower().StartsWith(_loginMarker);
        }

        private string GetClientName(string message)
        {
            return message.Trim().ToLower().Substring(_loginMarker.Length);
        }

        private void Login(Socket clientSocket, string message)
        {
            ClientInfo clientInfo = new ClientInfo
            {
                Socket = clientSocket,
                Name = GetClientName(message)
            };

            _clients.Add(clientInfo);
        }
    }
}
