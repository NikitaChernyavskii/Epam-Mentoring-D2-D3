using System;
using ChatApplication.Server;

namespace ServerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            server.Start();

            Console.ReadLine();
        }
    }
}
