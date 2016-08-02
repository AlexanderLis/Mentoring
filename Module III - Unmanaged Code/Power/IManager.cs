using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Power
{
    [ComVisible(true)]
    [Guid("AFFB1744-B41A-409E-A836-C7BE4A9D18C2")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IManager
    {
        SystemPowerInformation CallNtPowerInformation();

        void Hibernate();

        ulong CallLastSleepTime();

        ulong CallLastWakeTime();

        SystemBatteryState CallBatteryInformation();

        void ReserveHiberFile(bool toReserve);
    }
}
