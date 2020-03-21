using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class AnnonyTuples {
    static (double, double) GetValues() {
      return (1.5, 2.3);
    }

    public static void Run() {
      double x, y;
      (x, y) = GetValues();
      Console.WriteLine($"X={x}, Y={y}");
    }
  }
  }
