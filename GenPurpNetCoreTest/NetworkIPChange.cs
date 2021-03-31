using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Net;
using GenPurpNetCoreTest;

namespace GenPurpNetCoreTest {
  class IPChanger {
      // https: //stackoverflow.com/questions/209779/how-can-you-change-network-settings-ip-address-dns-wins-host-name-with-code    class NetworkManagement {
      public void setIP(string ip_address, string subnet_mask) {
        ManagementClass objMC =
          new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection objMOC = objMC.GetInstances();

        foreach (ManagementObject objMO in objMOC) {
          if ((bool)objMO["IPEnabled"]) {
            ManagementBaseObject setIP;
            ManagementBaseObject newIP =
              objMO.GetMethodParameters("EnableStatic");

            newIP["IPAddress"] = new string[] { ip_address };
            newIP["SubnetMask"] = new string[] { subnet_mask };

            setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
          }
        }
      }

      public void setGateway(string gateway) {
        ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection objMOC = objMC.GetInstances();

        foreach (ManagementObject objMO in objMOC) {
          if ((bool)objMO["IPEnabled"]) {
            ManagementBaseObject setGateway;
            ManagementBaseObject newGateway =
              objMO.GetMethodParameters("SetGateways");

            newGateway["DefaultIPGateway"] = new string[] { gateway };
            newGateway["GatewayCostMetric"] = new int[] { 1 };

            setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
          }
        }
      }

      public void setDNS(string NIC, string DNS) {
        ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection objMOC = objMC.GetInstances();

        foreach (ManagementObject objMO in objMOC) {
          if ((bool)objMO["IPEnabled"]) {
            // if you are using the System.Net.NetworkInformation.NetworkInterface
            // you'll need to change this line to
            // if (objMO["Caption"].ToString().Contains(NIC))
            // and pass in the Description property instead of the name 
            if (objMO["Caption"].Equals(NIC)) {
              ManagementBaseObject newDNS =
                objMO.GetMethodParameters("SetDNSServerSearchOrder");
              newDNS["DNSServerSearchOrder"] = DNS.Split(',');
              ManagementBaseObject setDNS =
                objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
            }
          }
        }
      }

  }

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
        Console.WriteLine($"Description : {ni.Description}");
        Console.WriteLine($"Id: {ni.Id}");
        Console.WriteLine($"Type: {ni.NetworkInterfaceType}");
        Console.WriteLine($"Phys. address: {ni.GetPhysicalAddress().ToString()}");
        Console.WriteLine($"Addresses count: {ni.GetIPProperties().UnicastAddresses.Count}");
        //IPInterfaceProperties properties = ni.GetIPProperties();
        int idx = 0;
        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses) {
          //if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
          Console.WriteLine($"Index: {idx}, Family: {ip.Address.AddressFamily}, Address: {ip.Address.ToString()}");
          idx++;
        }
        // ni.GetIPProperties().UnicastAddresses.Remove(ni.GetIPProperties().UnicastAddresses);
        // ni.GetIPProperties().UnicastAddresses.Add(new IPAddressInformation(new byte[] {192, 168, 1, 107}));
      }
      IPChanger nc = new IPChanger();
      nc.setIP("192.168.1.107", "255.255.255.0");
    }

  }
}
