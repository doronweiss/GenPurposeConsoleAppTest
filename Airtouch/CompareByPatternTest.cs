using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

  // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/patterns

namespace Airtouch {
  internal class Contnr {
    public int ival1;
    public int ival2;
    public string str;
  }

  internal class CompareByPatternTest {
    public static void Run() {
      Contnr cnt = new Contnr() { ival1 = 1, ival2 = 2, str = "doron" };
      bool good = cnt is { ival1: 1, ival2: 2 };
      Console.WriteLine($"Result = {good}");
      cnt.ival2 = 3;
      good = cnt is { ival1: 1, ival2: 2 };
      Console.WriteLine($"Result = {good}");
      cnt.ival2 = 2;
      good = cnt is { ival1: 1, ival2: 2 };
      Console.WriteLine($"Result = {good}");
    }
  }
}
