using MainService.Services;
using System.ServiceProcess;

namespace MainService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new MainService()
            //};
            //ServiceBase.Run(ServicesToRun);

            QueueService queueService = new QueueService();
            string m = queueService.ReceiveMessage();

            System.Console.WriteLine(m);

            System.Console.ReadLine();
        }
    }
}
