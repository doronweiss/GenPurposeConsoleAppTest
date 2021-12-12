using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class IComparableTest {
    public static bool IsInRange<T>(T item2test, T loLim, T hiLim) where T : IComparable {
      return item2test.CompareTo(loLim) >= 0 && item2test.CompareTo(hiLim) <= 0;
    }

    public static void Run() {
      int lowLim = 10, hiLim = 20;
      for (int idx=7; idx<25; idx++)
        Console.WriteLine($"Testing: {idx} => {IsInRange(idx, lowLim, hiLim)}");
    }
  }
}
