using System.Net.Sockets;

namespace ChatApplication.Common
{
    public interface IReadSocketResponseService
    {
        string EofMarker { get; }

        string GetStringMessage(Socket clientSocket);
    }
}
