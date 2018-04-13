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
            var lastWakeTime = sleepService.GetLastWakeTime();
            var systemBatteryState = sleepService.GetSystemBatteryState();
            var systemPowerInformation = sleepService.GetSystemPowerInformation();

            Console.ReadLine();
        }
    }
}
