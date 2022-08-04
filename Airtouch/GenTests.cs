using System;
using System.Collections.Generic;
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

    public static void Run() { 
      int i = Int32.MinValue;
      Console.WriteLine($"int: {i}");
      uint ui = BitConverter.ToUInt32(BitConverter.GetBytes(i), 0);
      Console.WriteLine($"uint: {ui}");
      //
      DemoCls dc1 = new DemoCls() {i = 2, j = 3};
      Console.WriteLine($"dc1: {dc1}");
      DemoCls dc2 = dc1 with {j = 4};
      Console.WriteLine($"dc1: {dc2}");
    }
  }
}
