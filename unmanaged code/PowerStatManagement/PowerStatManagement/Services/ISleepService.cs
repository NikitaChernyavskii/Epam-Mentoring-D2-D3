using PowerStateManagement.Models;
using System.Runtime.InteropServices;

namespace PowerStatManagement.Services
{
    [ComVisible(true)]
    [Guid("962042bf-92ae-45d0-b420-a637cf52d427")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface ISleepService
    {
        long GetLastSleepTime();
        long GetLastWakeTime();
        SystemBatteryState GetSystemBatteryState();
        SystemPowerInformation GetSystemPowerInformation();
    }
}