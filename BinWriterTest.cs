using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class BinWriterTest {
    private static  AsyncBinWriter abw;
    public static void Run() {
      abw = new AsyncBinWriter("junk.bin");
      abw.Start(false);
      Random r = new Random();
      for (int idx = 0; idx < 100; idx++) {
        int[] dds = Enumerable.Range(0, 20).Select(x => x).ToArray();
        abw.AddIntegers(dds);
      }
      Thread.Sleep(100);
      abw.Stop();
      Console.WriteLine("finished");
    }
  }
}
