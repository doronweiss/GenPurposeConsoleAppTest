using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class CSParallelism {
    public static void Run() {
      // parallelism
      long count = 100000000;
      Stopwatch sw = new Stopwatch();
      long sum = 0;
      double[] dt = new double[count];
      for (int idx = 0; idx < count; idx++)
        dt[idx] = idx;
      // linear
      sw.Reset();
      sum = 0;
      sw.Start();
      for (int idx = 0; idx < count; idx++)
        dt[idx] = Math.Sin(dt[idx]);
      sw.Stop();
      double dsum = 0;
      for (int idx = 0; idx < count; idx++)
        dsum += dt[idx];
      Console.WriteLine("Single:: Count: {0}, Sum: {1}, Elapsed: {2}", count, dsum, sw.ElapsedMilliseconds);
      // parallel
      sw.Reset();
      for (int idx = 0; idx < count; idx++)
        dt[idx] = idx;
      sw.Start();
      System.Threading.Tasks.Parallel.For(0, count, s => { dt[s] = Math.Sin(dt[s]); });
      sw.Stop();
      dsum = 0;
      for (int idx = 0; idx < count; idx++)
        dsum += dt[idx];
      Console.WriteLine("Parallel:: Count: {0}, Sum: {1}, Elapsed: {2}", count, dsum, sw.ElapsedMilliseconds);
    }
  }
}
