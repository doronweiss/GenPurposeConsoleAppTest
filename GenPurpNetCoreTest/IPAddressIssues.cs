using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class IPAddressIssues {

    public static bool OnSameNet(IPAddress addr1, IPAddress addr2) {
      byte[] addr1Bytes = addr1.GetAddressBytes();
      byte[] addr2Bytes = addr2.GetAddressBytes();
      return addr1Bytes[0] == addr2Bytes[0] && addr1Bytes[1] == addr2Bytes[1] && addr1Bytes[2] == addr2Bytes[2];
    }

    public static (string, string) GetLocalIp(string plcIpAddress) {
      IPAddress plcIp = IPAddress.Parse(plcIpAddress);
      string localIP = "127.0.0.1"; //"127.0.0.1";
      NetworkInterface[] devs = NetworkInterface.GetAllNetworkInterfaces();
      foreach (NetworkInterface inf in devs) {
        if (inf.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || inf.NetworkInterfaceType == NetworkInterfaceType.Ethernet) {
          foreach (UnicastIPAddressInformation address in inf.GetIPProperties().UnicastAddresses) {
            if (address.Address.AddressFamily == AddressFamily.InterNetwork) {
              Console.WriteLine($"[NetworkInterface] Found ip address: {address.Address}");
              if (OnSameNet(address.Address, plcIp))
                Console.WriteLine($"PLC network address: {address.Address}");
              else {
                Console.WriteLine($"External network address: {address.Address}");
                return (address.Address.ToString(), inf.GetPhysicalAddress().ToString());
              }
            }
          }
        }
      }
      return ("", "");;
    }


    public static void Run() {
      Console.WriteLine(GetLocalIp("192.168.0.250"));
    }
  }
}
