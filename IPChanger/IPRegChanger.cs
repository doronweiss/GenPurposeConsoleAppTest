using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace IPChanger {
  static class IPRegChanger {
    public static string GetLLocalLanEthernetID(string interfaceName) {
      string res = "";
      try {
        NetworkInterface[] networkCards
          = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface intrfc in networkCards) {
          if (string.Compare(intrfc.Name, interfaceName, true)==0) {
            res = intrfc.Id;
            break;
          }
        }
      } catch (Exception ex) {
        res = "";
      }
      return res;
    }

    public static bool ChangeIPAddress(string regKeyPath, bool isDHCP, string ip, string subnet, string gateway) {
      string message = $"{(isDHCP ? "DHCP" : "static")}, {ip}/{subnet}/{gateway}";
      try {
        RegistryKey rk = Registry.LocalMachine.OpenSubKey(regKeyPath, true);
        rk.SetValue("EnableDhcp", isDHCP ? 1 : 0);
        rk.SetValue("IpAddress", isDHCP ? "" : ip);
        rk.SetValue("SubnetMask", isDHCP ? "" : subnet);
        rk.SetValue("DefaultGateway", isDHCP ? "" : gateway);
        rk.Close();
      } catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine($"error: {ex.Message}");
        return false;
      }
      return true;
    }

  }
}
