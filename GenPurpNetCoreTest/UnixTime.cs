using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class UnixTime {
    public static void Run() {
      DateTime nw = DateTime.Now;
      long ut = new DateTimeOffset(nw).ToUnixTimeSeconds();
      byte[] btd = BitConverter.GetBytes(ut);
      int dt = BitConverter.ToInt32(btd,0);
      int ht = BitConverter.ToInt32(btd,4);
      byte[] newbtd = new byte[8];
      Array.Copy(BitConverter.GetBytes(dt), newbtd, 4);
      DateTime rdt = DateTimeOffset.FromUnixTimeSeconds(BitConverter.ToInt64(newbtd, 0)).DateTime;
      newbtd = new byte[8];
      Array.Copy(BitConverter.GetBytes(ht), 0, newbtd, 4, 4);
      DateTime rht = DateTimeOffset.FromUnixTimeSeconds(BitConverter.ToInt64(newbtd, 0)).DateTime;
      Console.WriteLine($"Started: {nw} Date: {rdt}, time: {rht}");
    }
  }
}
