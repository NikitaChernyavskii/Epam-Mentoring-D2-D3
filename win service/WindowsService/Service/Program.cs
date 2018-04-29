using System.ServiceProcess;
using Service.Services;
using System;

namespace ServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileSystemWatcherService test = new FileSystemWatcherService();
            //test.WatchFolder();

            //Console.ReadLine();

            ServiceBase.Run(new FileService());
        }
    }
}
