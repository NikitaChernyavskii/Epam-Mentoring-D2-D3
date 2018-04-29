using System;
using Service.Services;

namespace ServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcherService test = new FileSystemWatcherService();
            test.WatchFolder();

            Console.ReadLine();
        }
    }
}
