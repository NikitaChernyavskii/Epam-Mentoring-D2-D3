using System.ServiceProcess;
using Service.Services;

namespace ServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase.Run(new FileService());
        }
    }
}
