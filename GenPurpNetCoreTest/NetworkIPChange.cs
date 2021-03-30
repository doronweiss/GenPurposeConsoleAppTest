using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {

  class NetworkIPChange {

    public static NetworkInterface FindlLocalLan1EthernetID(string intrfcName) {
      string res = "";
      try {
        System.Net.NetworkInformation.NetworkInterface[] networkCards
          = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface intrfc in networkCards) {
          if (intrfc.Name == intrfcName) {
            return intrfc;
          }
        }
      } catch (Exception ex) {
        res = "";
      }
      return null;
    }

    public static void Run() {
      NetworkInterface ni = FindlLocalLan1EthernetID("Ethernet");
      if (ni != null) { 
        Console.WriteLine(ni.Description);
        Console.WriteLine(ni.Id);
        Console.WriteLine(ni.NetworkInterfaceType);
        Console.WriteLine(ni.GetPhysicalAddress().ToString());
        IPInterfaceProperties properties = ni.GetIPProperties();
      }
    }

  }
}
