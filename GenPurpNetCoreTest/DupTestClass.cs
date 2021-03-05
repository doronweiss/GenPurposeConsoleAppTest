using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class DupTestClass {
    private static List<double> xs = new List<double>() { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0 };

    public static void
      Run() {
      List<double> ys = xs.Select(x => Math.Sin(x)).ToList();
      List<double> zs = xs.Select(x => Math.Cos(x)).ToList();
      double lookupVal = 4.75;
      int idx = xs.FindLastIndex(x => x < lookupVal);
      double frac = (xs[idx + 1] - lookupVal) / (xs[idx + 1] - xs[idx]);
      double y = ys[idx] + frac * (ys[idx + 1] - ys[idx]);
      double zbad = zs[idx] + frac * (ys[idx + 1] - ys[idx]); // duplicated
      double zgood = zs[idx] + frac * (zs[idx + 1] - zs[idx]); // duplicated
      Console.WriteLine($"Lookup: {lookupVal} => Y: {y}, Z Bad: {zbad}, Z Good: {zgood}");
    }
  }
}
