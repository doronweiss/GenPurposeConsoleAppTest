using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class LinqTests {
    public static void Run() {
//      List<string> parts = new List<string>() {"1.5", "3.75", "12.45", "2.43"};
//      List<double> dbls = parts.Select(x => Double.Parse(x)).ToList();
//      for (int idx=0;idx<dbls.Count; idx++)
//        Console.WriteLine($"Part[{idx}]={dbls[idx]}");
//      dbls = parts.Select(Double.Parse).ToList();
//      for (int idx = 0; idx < dbls.Count; idx++)
//        Console.WriteLine($"Part[{idx}]={dbls[idx]}");
      List<double> ld = new List<double>() {1.0, 2.0, 3.0, 4.0, 3.5, 6.0};
      List<double> ld2 = ld.Where(x => x > 3.0).Select(x => x * x).ToList();
      foreach (double x in ld2) {
        Console.WriteLine($"{x}");
      }
    }
  }
}
