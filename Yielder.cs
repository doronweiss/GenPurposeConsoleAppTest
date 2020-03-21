using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class Yielder {
    static IEnumerable<int> Get() {
      var i = 0;
      int prev = 1;
      int prev2 = 0;
      while (true) {
        int t = prev + prev2;
        prev2 = prev;
        prev = t;
        yield return t;
      }
      yield break;
    }

    public static void Run() {
      foreach (var number in Get().TakeWhile(x => x<25))
        Console.WriteLine(number);
    }
  }
}
