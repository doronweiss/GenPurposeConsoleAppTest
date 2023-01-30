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

// https://code-maze.com/csharp-task-run-vs-task-factory-startnew/

namespace UtilsClassesTests {
  internal class ARPTools {
    [DllImport("iphlpapi.dll", ExactSpelling = true)]
    [SecurityCritical]
    private static extern int SendARP(int destinationIp, int sourceIp, byte[] macAddress, ref int physicalAddrLength);

    private static (IPAddress, PhysicalAddress) Lookup(IPAddress ip) {
      if (ip == null)
        throw new ArgumentNullException(nameof(ip));
      int destIp = BitConverter.ToInt32(ip.GetAddressBytes(), 0);
      var addr = new byte[6];
      var len = addr.Length;
      var res = SendARP(destIp, 0, addr, ref len);
      return res == 0
        ? (ip, new PhysicalAddress(addr))
        : (ip, new PhysicalAddress(new byte[] { 0, 0, 0, 0 }));
    }

    public static async void Run() {
      List<Task<(IPAddress, PhysicalAddress)>> tasks = new ();
      for (int idx = 0; idx < 255; idx++) {
        IPAddress ip = new IPAddress(new byte[] { 192, 168, 0, (byte)idx });
        //Task<(IPAddress, PhysicalAddress)> pa = Task.Run(() => Lookup(ip));
        Task<(IPAddress, PhysicalAddress)> pa = Task.Factory.StartNew(() => Lookup(ip));
        tasks.Add(pa);
        Console.WriteLine($"Started task {idx}");
      }
      Task.WaitAll(tasks.ToArray());
      foreach (Task<(IPAddress, PhysicalAddress)> tres in tasks) {
        (IPAddress, PhysicalAddress) tt = tres.Result;
        Console.WriteLine($"Address: {tt.Item1} physical address: {tt.Item2}");
      }
    }
  }
}
