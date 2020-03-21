using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class LinqThings {
    public static void Run() {
      // fibs under 100
      var numbers = new[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };
      // get every other fib number 
      //var everyOtherFib = numbers.Where((n, index) => (index % 2) == 0).ToList();
      var everyOtherFib = numbers.Select((n, index) => (index == 0) ? n : n - numbers[index - 1]).ToList();
      foreach (var i in everyOtherFib)
        Console.WriteLine($"Num={i}");
    }
  }
}
