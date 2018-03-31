using System.Net.Sockets;
using System.Text;

namespace ChatApplication.Common
{
    public class ReadSocketResponseService : IReadSocketResponseService
    {
        public string EofMarker { get; private set; }

        public ReadSocketResponseService()
        {
            EofMarker = "<EOF>";
        }

        public string GetStringMessage(Socket clientSocket)
        {
            string message = "";
            while (true)
            {
                var bytes = new byte[1024];
                int bytesRec = clientSocket.Receive(bytes);
                message += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (message.IndexOf(EofMarker) > -1)
                {
                    break;
                }
            }

            return message;
        }
    }
}
