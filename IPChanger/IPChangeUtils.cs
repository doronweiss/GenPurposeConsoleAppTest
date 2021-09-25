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
    public static bool SetNetworkConfig(string adapter, bool isDHCP, string ipAddress, string subnetMak, string gateway, string dns1, string dns2) {
      //ManagementObjectSearcher mos = new ManagementObjectSearcher($"Select * From Win32_NetworkAdapter Where NetConnectionID = '{adapter}'");
      //ManagementObjectSearcher mos = new ManagementObjectSearcher($"Select * From Win32_NetworkAdapterConfiguration");// Where NetConnectionID = '{adapter}'");
      ManagementObjectSearcher mos = new ManagementObjectSearcher($"Select * From Win32_NetworkAdapter");// Where NetConnectionID = '{adapter}'");
      var nia = mos.Get();
      if (nia == null || nia.Count == 0)
        return false;
      var enumer = nia.GetEnumerator();
      dynamic nicCfg = null;
      bool found = false;
      int first = 0;
      while (enumer.MoveNext()) {
        nicCfg = enumer.Current;
        //System.Diagnostics.Debug.WriteLine($"Name = {nic.Properties.Count}");
        var enumerP = enumer.Current.Properties.GetEnumerator();
        while (enumerP.MoveNext()) {
          dynamic prop = enumerP.Current;
          //System.Diagnostics.Debug.WriteLine($"{first}:{prop.Name} = {prop.Value}");
          if (prop.Name == "NetConnectionID") {
            System.Diagnostics.Debug.WriteLine($"{prop.Name} = {prop.Value}");
            if (string.Compare((prop.Value?.ToString() ?? ""), adapter, true) == 0)
              found = true;
          }
        }
        if (found)
          break;
        first++;
      }
      if (!found)
        return false;
      var nia2 = nicCfg.GetRelated("Win32_NetworkAdapterConfiguration");
      if (nia2 == null || nia2.Count == 0)
        return false;
      dynamic cfgEnumer = nia2.GetEnumerator();
      if (!cfgEnumer.MoveNext())
        return false;
      dynamic nic = cfgEnumer.Current;
      // {
      //   var enumerP = nic.Methods.GetEnumerator();
      //   while (enumerP.MoveNext()) {
      //     dynamic prop = enumerP.Current;
      //     System.Diagnostics.Debug.WriteLine($"{first}:{prop.Name} = {prop.Value}");
      //   }
      // }
      //return false;
      object res;
      if (isDHCP)
        res = nic.InvokeMethod("EnableDHCP", null);
      else {
        ManagementBaseObject newIP =
          nic.GetMethodParameters("EnableStatic");
        newIP["IPAddress"] = new string[] { ipAddress };
        newIP["SubnetMask"] = new string[] { subnetMak };
        res = nic.InvokeMethod("EnableStatic", newIP, null);
        if ((res is int ires) && ires > 1)
          return false;
        ManagementBaseObject newGW =
          nic.GetMethodParameters("SetGateways");
        newGW["DefaultIPGateway"] = new string[] { gateway };
        newGW["GatewayCostMetric"] = new int[] { 1 };
        res = nic.InvokeMethod("SetGateways", newGW, null);
        if ((res is int ires2) && ires2 > 1)
          return false;
        return true;
      }
      return false;
    }
  }
}
