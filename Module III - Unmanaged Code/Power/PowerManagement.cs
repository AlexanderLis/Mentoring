using System;
using System.Runtime.InteropServices;

namespace Power
{
    public class PowerManagement
    {
        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation")]
        public static extern void CallNtPowerInformation(int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            out SystemPowerInformation spi,
            int nOutputBufferSize);

        [DllImport("powrprof.dll", EntryPoint = "SetSuspendState")]
        public static extern void SetSuspendState(bool hibernate,
            bool forceCritical,
            bool disableWeakEvent);

        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation")]
        public static extern void CallSleepInformation(int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            out ulong time,
            int nOutputBufferSize);

        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation")]
        public static extern void CallBatteryInformation(int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            out SystemBatteryState batteryInfo,
            int nOutputBufferSize);

        [DllImport("powrprof.dll", EntryPoint = "CallNtPowerInformation")]
        public static extern void ReserveHiberFile(int InformationLevel,
            bool lpInputBuffer,
            int nInputBufferSize,
            bool info,
            int nOutputBufferSize);
    }

    public struct SystemBatteryState
    {
        public bool AcOnLine;
        public bool BatteryPresent;
        public bool Charging;
        public bool Discharging;
        public bool[] Spare1;
        public uint MaxCapacity;
        public uint RemainingCapacity;
        public uint Rate;
        public uint EstimatedTime;
        public uint DefaultAlert1;
        public uint DefaultAlert2;

    }

    public struct SystemPowerInformation
    {
        public uint MaxIdlenessAllowed;
        public uint Idleness;
        public uint TimeRemaining;
        public byte CoolingMode;
    }
}
