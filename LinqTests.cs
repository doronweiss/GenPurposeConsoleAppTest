using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class LinqTests {
    public static void Run() {
      List<string> parts = new List<string>() { "1.5", "3.75", "12.45", "2.43" };
      // explicit parsing
      List<double> dbls = parts.Select(x => Double.Parse(x)).ToList();
      for (int idx = 0; idx < dbls.Count; idx++)
        Console.WriteLine($"Part[{idx}]={dbls[idx]}");
      // implicit parcing
      dbls = parts.Select(Double.Parse).ToList();
      for (int idx = 0; idx < dbls.Count; idx++)
        Console.WriteLine($"Part[{idx}]={dbls[idx]}");
      // operation
      List<double> ld = new List<double>() { 1.0, 2.0, 3.0, 4.0, 3.5, 6.0 };
      List<double> ld2 = ld.Where(x => x > 3.0).Select(x => x * x).ToList();
      foreach (double x in ld2) {
        Console.WriteLine($"{x}");
      }
      // fibs under 100
      var numbers = new[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };
      // get every other fib number 
      var everyOtherFib = numbers.Select((n, index) => (index == 0) ? n : n - numbers[index - 1]).ToList();
      foreach (var i in everyOtherFib)
        Console.WriteLine($"Num={i}");
    }
  }
}
