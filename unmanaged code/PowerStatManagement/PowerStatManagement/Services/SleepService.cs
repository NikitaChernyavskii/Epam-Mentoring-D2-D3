using System;
using System.Runtime.InteropServices;
using PowerStateManagement.Models;

namespace PowerStateManagement.Services
{
    public class SleepService
    {
        private const int SystemBatteryState = 5;
        private const int SystemPowerInformation = 12;
        private const int LastWakeTime = 14;
        private const int LastSleepTime = 15;

        public long GetLastSleepTime()
        {
            return GetTime(LastSleepTime);
        }

        public long GetLastWakeTime()
        {
            return GetTime(LastWakeTime);
        }

        public SystemBatteryState GetSystemBatteryState()
        {
            IntPtr systemBatteryStatePtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemBatteryState)));
            int status = CallNtPowerInformation(SystemBatteryState, IntPtr.Zero, 0, systemBatteryStatePtr,
                Marshal.SizeOf(typeof(SystemBatteryState)));
            SystemBatteryState btState = (SystemBatteryState)Marshal.PtrToStructure(systemBatteryStatePtr, typeof(SystemBatteryState));
            Marshal.FreeHGlobal(systemBatteryStatePtr);

            return btState;
        }

        public SystemPowerInformation GetSystemPowerInformation()
        {
            IntPtr systemBatteryStatePtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SystemPowerInformation)));
            int status = CallNtPowerInformation(SystemPowerInformation, IntPtr.Zero, 0, systemBatteryStatePtr,
                Marshal.SizeOf(typeof(SystemPowerInformation)));
            SystemPowerInformation spInformation = (SystemPowerInformation)Marshal.PtrToStructure(systemBatteryStatePtr, typeof(SystemPowerInformation));
            Marshal.FreeHGlobal(systemBatteryStatePtr);
            return spInformation;
        }

        private long GetTime(int timeLookUp)
        {
            IntPtr lastSleepTimePtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(long)));
            int status = CallNtPowerInformation(timeLookUp, IntPtr.Zero, 0, lastSleepTimePtr,
                Marshal.SizeOf(typeof(ulong)));

            long lastSleepTimeValue = (long)Marshal.ReadIntPtr(lastSleepTimePtr);
            Marshal.FreeHGlobal(lastSleepTimePtr);
            return lastSleepTimeValue;
        }

        [DllImport("powrprof.dll", SetLastError = true)]
        private static extern int CallNtPowerInformation(int informationLevel, IntPtr lpInputBuffer,
            int nInputBufferSize, IntPtr lpOutputBuffer, int nOutputBufferSize);
    }
}
