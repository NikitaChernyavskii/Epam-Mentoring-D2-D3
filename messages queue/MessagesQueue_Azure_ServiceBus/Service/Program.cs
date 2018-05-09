using System.ServiceProcess;
using Service.Services;

namespace ServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FileSystemWatcherService s = new FileSystemWatcherService();
            s.WatchFolder();
            //ServiceBase.Run(new FileService());


            System.Console.ReadLine();
        }
    }
}
