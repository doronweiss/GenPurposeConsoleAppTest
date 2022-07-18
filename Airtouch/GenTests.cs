using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airtouch {
  internal class GenTests {
    public static void Run() { 
      int i = Int32.MinValue;
      Console.WriteLine($"int: {i}");
      uint ui = BitConverter.ToUInt32(BitConverter.GetBytes(i), 0);
      Console.WriteLine($"uint: {ui}");
    }
  }
}
