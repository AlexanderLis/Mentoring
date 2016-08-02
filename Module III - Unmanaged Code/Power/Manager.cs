using System;
using System.Runtime.InteropServices;

namespace Power
{
    [ComVisible(true)]
    [Guid("ADDE4BA8-293B-48F2-A197-F9263B23259B")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Manager : IManager
    {
        private int LastSleepTime = 15;
        private int LastWakeTime = 14;
        private int SystemPowerInformation = 12;
        private int SystemBatteryState = 5;
        private int SystemReserveHiberFile = 10;

        public SystemPowerInformation CallNtPowerInformation()
        {
            SystemPowerInformation spi;
            PowerManagement.CallNtPowerInformation(SystemPowerInformation, IntPtr.Zero, 0, out spi, Marshal.SizeOf(typeof(SystemPowerInformation)));
            return spi;
        }

        public void Hibernate()
        {
            PowerManagement.SetSuspendState(true, false, false);
        }

        public ulong CallLastSleepTime()
        {
            ulong time;
            PowerManagement.CallSleepInformation(LastSleepTime, IntPtr.Zero, 0, out time, Marshal.SizeOf(typeof(ulong)));
            return time;
        }

        public ulong CallLastWakeTime()
        {
            ulong time;
            PowerManagement.CallSleepInformation(LastWakeTime, IntPtr.Zero, 0, out time, Marshal.SizeOf(typeof(ulong)));
            return time;
        }

        public SystemBatteryState CallBatteryInformation()
        {
            SystemBatteryState batteryInfo;
            PowerManagement.CallBatteryInformation(SystemBatteryState, IntPtr.Zero, 0, out batteryInfo, Marshal.SizeOf(typeof(SystemBatteryState)));
            return batteryInfo;
        }

        public void ReserveHiberFile(bool toReserve)
        {
            var info = false;
            PowerManagement.ReserveHiberFile(SystemReserveHiberFile, toReserve, Marshal.SizeOf(typeof(bool)), info, Marshal.SizeOf(typeof(bool)));
        }
    }
}
