using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GenPurposeConsoleAppTest {
  class CSGeneral {
    public static void Run() {
      int i = 1;
      for (int idx = 0; idx < 100; idx++) {
        Console.WriteLine($"2^{idx} = {i}");
        i *= 2;
      }
    }
  }
}
