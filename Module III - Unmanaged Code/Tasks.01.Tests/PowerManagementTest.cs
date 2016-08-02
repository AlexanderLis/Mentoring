using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Power;

namespace Tasks._01.Tests
{
    [TestClass]
    public class PowerManagementTest
    {
        private int LastSleepTime = 15;
        private int LastWakeTime = 14;
        private int SystemPowerInformation = 12;
        private int SystemBatteryState = 5;
        private int SystemReserveHiberFile = 10;

        [TestMethod]
        public void LastSleepTimeTest()
        {
            ulong time;
            PowerManagement.CallSleepInformation(LastSleepTime, IntPtr.Zero, 0, out time,
                Marshal.SizeOf(typeof(ulong)));
            Console.WriteLine("Last sleep time: " + time);

            PowerManagement.CallSleepInformation(LastWakeTime, IntPtr.Zero, 0, out time,
                Marshal.SizeOf(typeof(ulong)));
            Console.WriteLine("Last wake time: " + time);
            
        }

        [TestMethod]
        public void CallPowerInformationTest()
        {
            SystemPowerInformation spi;
            PowerManagement.CallNtPowerInformation(SystemPowerInformation, IntPtr.Zero, 0, out spi,
                Marshal.SizeOf(typeof (SystemPowerInformation)));
                Console.WriteLine("Time remaining: " + spi.TimeRemaining);
                Console.WriteLine("Idleness: " + spi.Idleness);
                Console.WriteLine("Cooling mode: " + spi.CoolingMode);
                Console.WriteLine("Max idleness allowed: " + spi.MaxIdlenessAllowed);
        }

        [TestMethod]
        public void CallBatteryInformationTest()
        {
            SystemBatteryState sbs;
            PowerManagement.CallBatteryInformation(SystemBatteryState, IntPtr.Zero, 0, out sbs,
                Marshal.SizeOf(typeof(SystemPowerInformation)));
            Console.WriteLine("Battery Present: " + sbs.BatteryPresent);
            Console.WriteLine("Remaining Capacity: " + sbs.RemainingCapacity);
            Console.WriteLine("Max Capacity: " + sbs.MaxCapacity);
            Console.WriteLine("Rate: " + sbs.Rate);
        }

        [TestMethod]
        public void ReserveHiberFileTest()
        {
            bool outParam = false;
            PowerManagement.ReserveHiberFile(SystemReserveHiberFile, false, 32, outParam,
                Marshal.SizeOf(typeof(bool)));
        }

        [TestMethod]
        public void SetSuspendStateTest()
        {
            PowerManagement.SetSuspendState(true, false, false);
        }

        [TestMethod]
        public void HibernateTest()
        {
            var pm = new Manager();
            pm.Hibernate();
        }

        [TestMethod]
        public void CallLastSleepTest()
        {
            var pm = new Manager();
            Console.WriteLine(pm.CallLastSleepTime());
        }

        [TestMethod]
        public void ReserveTest()
        {
            var pm = new Manager();
            pm.ReserveHiberFile(false);
        }
    }
}
