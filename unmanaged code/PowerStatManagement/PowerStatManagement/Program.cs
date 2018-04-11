using System;
using PowerStateManagement.Services;

namespace PowerStatManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            SleepService sleepService = new SleepService();

            var lastSleepTime = sleepService.GetLastSleepTime();
            Console.WriteLine(lastSleepTime);

            var lastWakeTime = sleepService.GetLastWakeTime();
            Console.WriteLine(lastWakeTime);

            var systemBatteryState = sleepService.GetSystemBatteryState();

            var systemPowerInformation = sleepService.GetSystemPowerInformation();

            Console.ReadLine();
        }
    }
}
