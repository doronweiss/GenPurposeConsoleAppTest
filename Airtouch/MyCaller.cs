using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airtouch {
  internal class MyCaller {
    private static int counter = 0;
    private static void func1() {
      counter++;
      StackTrace stackTrace = new StackTrace();
      Console.WriteLine($"In {stackTrace.GetFrame(0).GetMethod().Name}, called from: {stackTrace.GetFrame(1).GetMethod().Name}");
      switch (counter) {
        case <=1:
          func2();
          break;
        case 2:
          func1();
          break;
        case >= 3:
          return;
      }
    }

    private static void func2() {
      func1();
    }

    public static void Run() {
      func1();
    }
  }
}
