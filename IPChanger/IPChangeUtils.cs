using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

//https://docs.microsoft.com/en-us/windows/win32/wmisdk/wmi-tasks--networking
//https://docs.microsoft.com/en-us/dotnet/api/system.management.managementobjectsearcher?view=dotnet-plat-ext-5.0

namespace IPChanger {
  static class IPChangeUtils {
    public static bool SetNetworkConfig(string adapter, bool isDHCP, string ipAddress, string gateway, string dns1, string dns2) {
      ManagementObjectSearcher mos = new ManagementObjectSearcher($"Select * From Win32_NetworkAdapter Where NetConnectionID = '{adapter}'");
      var nia = mos.Get();
      if (nia != null && nia.Count > 0)
        return false;

      return nia != null && nia.Count>0;
    }
  }
}
