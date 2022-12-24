using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airtouch {
  internal class GenTests {

    record class DemoCls {
      public int i;
      public int j;

      public override string ToString () =>
        $"Demo class, i: {i}, j: {j}";
    }

    class EnTest {
      List<int> data = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
      public IEnumerable<int> GetEnumerator() {
        foreach (int rbt in data) {
          yield return rbt;
        }
      }
    }

    public static void Run() { 
      // int i = Int32.MinValue;
      // Console.WriteLine($"int: {i}");
      // uint ui = BitConverter.ToUInt32(BitConverter.GetBytes(i), 0);
      // Console.WriteLine($"uint: {ui}");
      // //
      // DemoCls dc1 = new DemoCls() {i = 2, j = 3};
      // Console.WriteLine($"dc1: {dc1}");
      // DemoCls dc2 = dc1 with {j = 4};
      // Console.WriteLine($"dc1: {dc2}");

      // EnTest ent = new EnTest();
      // foreach (int i in ent.GetEnumerator())
      //   Console.WriteLine($"val={i}");
      // foreach (int i in ent.GetEnumerator())
      //   Console.WriteLine($"val={i}");

      string str = "2022-12-14 14:31:28.4744"; //4|DEBUG|main|Car Started 12/14/2022 2:31:28 PM |";
      CultureInfo provider = CultureInfo.InvariantCulture;
      DateTime dt = DateTime.ParseExact(str, "yyyy-MM-dd HH:mm:ss.ffff", provider);
      Console.WriteLine(dt);
    }
  }
}
