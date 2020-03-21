using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class InheritanceTest {
    class abase {
      public static string name = "Base";
    }

    class deriv1 : abase {
      public static string name = "deriv1";
    }

    class deriv2 : abase {
      public static string name = "deriv2";
    }

    public static void Run() {
      Console.WriteLine($"Base: {abase.name}, deriv1: {deriv1.name}, deriv2: {deriv2.name}");
      abase.name = "new base name";
      Console.WriteLine($"Base: {abase.name}, deriv1: {deriv1.name}, deriv2: {deriv2.name}");
    }
  }
}
