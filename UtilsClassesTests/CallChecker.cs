using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsClassesTests {
  internal class CallChecker {

    public static bool IsRecursive() {
      StackTrace stackTrace = new StackTrace();
      return stackTrace.GetFrame(1).GetMethod() != stackTrace.GetFrame(2).GetMethod() ? false : true;
    }

    public static void Testme() {
      Console.WriteLine($"Called recursively: {IsRecursive()}");
      if (! IsRecursive())
        Testme();
    }

    public static void Run() {
      Testme();
    }
  }
}
