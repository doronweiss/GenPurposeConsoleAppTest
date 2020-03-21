#define TEST2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class CastTester {
    public static void Run() {
#if TEST1
      ulong uls = ulong.MaxValue-1;
      long l = unchecked((long)uls);
      ulong ule = unchecked((ulong)l);
      Console.WriteLine ($"started with: {uls} => { l} => { ule}");

      byte[] bts = new byte[] { 1, 2, 3, 4, 5 };
      Console.WriteLine($"Before");
      for (int idx=0; idx<bts.Length; idx++)
        Console.WriteLine($"BTS[{idx}]={bts[idx]}");
      Array.ConvertAll<byte, byte>(bts, b => b=0);
      Console.WriteLine($"After");
      for (int idx = 0; idx < bts.Length; idx++)
        Console.WriteLine($"BTS[{idx}]={bts[idx]}");
#endif
#if TEST2
      uint ui = 0xe0001234;
      int i = unchecked((int)ui);
      ulong ul = unchecked((ulong)ui);
      Console.WriteLine($"ui={ui}, i={i}, ul={ul}");
#endif
    }
  }
}
