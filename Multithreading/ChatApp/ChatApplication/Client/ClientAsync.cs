using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ChatApplication.Models;

namespace ChatApplication.Client
{
    public class ClientAsync
    {
        private const int _port = 3000;
        private IPHostEntry _ipHostInfo;
        private IPAddress _ipAddress;
        private IPEndPoint _remoteEp;
        private Socket _clientSocket;
        private string _userName;
        byte[] _data = new byte[1024];

        public ClientAsync(string userName)
        {
            _ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            _ipAddress = _ipHostInfo.AddressList[0];
            _remoteEp = new IPEndPoint(_ipAddress, _port);
            _clientSocket = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _userName = userName;

            StartClient();
        }

        public void SendMessage(string message)
        {
            try
            {
                _clientSocket.BeginConnect(_remoteEp, ConnectCallback, _clientSocket);
                Send(message);
                Receive();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        private void StartClient()
        {
            _clientSocket.BeginConnect(_remoteEp, ConnectCallback, _clientSocket);
            Send($"login{_userName}");
            Receive();
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Send(string message)
        {
            byte[] byteData = Encoding.Default.GetBytes(message);
            _clientSocket.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, _clientSocket);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void Receive()
        {
            try
            {
                _clientSocket.BeginReceive(_data, 0, _data.Length, 0, ReceiveCallback, _clientSocket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                _clientSocket.EndReceive(ar);
                var response = Encoding.Default.GetString(_data);
                Console.WriteLine(response);
                //_clientSocket.Disconnect(true);
                _clientSocket.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
