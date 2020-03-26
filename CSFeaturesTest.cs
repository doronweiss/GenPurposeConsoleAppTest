using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://blogs.msdn.microsoft.com/dotnet/2017/03/09/new-features-in-c-7-0/

namespace GenPurposeConsoleAppTest {
  class CSFeaturesTest {
    static (string name, double age) FetchIt() {
      return ("doron", 57.75);
    }

    public static void Run() {
      // variable name
      int a = 3;
      Console.WriteLine($"{nameof(a)}");
      // anonymous tuples
      (string name, double age) fret = FetchIt();
      Console.WriteLine($"Received: {fret.name} / {fret.age}");
    }
  }
}
