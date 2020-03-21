using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class InterpolationTest {
    public static int LIndex(List<double> lst, double x, int prevIdx) {
      int maxSz = lst.Count - 2;
      int idx = prevIdx;
      while (lst[idx + 1] <= x && idx < maxSz)
        idx++;
      while (lst[idx] > x && idx > 0)
        idx--;
      return idx;
    }

    public static void Run() {
      List<double> dbls = new List<double>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
      int lidx = 0;
      for (int idx = -3; idx < 14; idx++) {
        lidx = LIndex(dbls, (double) idx, lidx);
        Console.WriteLine($"X:{(double) idx}, index:{lidx}");
      }
    }
  }
}
