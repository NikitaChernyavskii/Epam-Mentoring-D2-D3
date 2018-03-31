using System;
using ChatApplication.Client;

namespace ClientConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name");
            string clientName = Console.ReadLine();
            Client client = new Client();
            client.Start(clientName);
            while (true)
            {
                var message = Console.ReadLine();
                client.SendMessage(message);

                Console.ReadLine();
            }
        }
    }
}
