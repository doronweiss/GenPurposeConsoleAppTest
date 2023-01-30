using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace UtilsClassesTests {
  internal class ARPTools {
    [DllImport("iphlpapi.dll", ExactSpelling = true)]
    [SecurityCritical]
    internal static extern int SendARP(int destinationIp, int sourceIp, byte[] macAddress, ref int physicalAddrLength);

    public static PhysicalAddress Lookup(IPAddress ip) {
      if (ip == null)
        throw new ArgumentNullException(nameof(ip));
      int destIp = BitConverter.ToInt32(ip.GetAddressBytes(), 0);
      var addr = new byte[6];
      var len = addr.Length;
      var res = SendARP(destIp, 0, addr, ref len);
      return res == 0
        ? new PhysicalAddress(addr)
        : new PhysicalAddress(new byte[] { 0, 0, 0, 0 });
    }

    public static void Run() {
      for (int idx = 0; idx < 100; idx++) {
        IPAddress ad = new IPAddress(new byte[] {192, 168, 0, (byte) idx});
        PhysicalAddress pa = Lookup(ad);
        Console.WriteLine($"Address: {ad} physical address: {pa}");
      }
    }
  }
}
