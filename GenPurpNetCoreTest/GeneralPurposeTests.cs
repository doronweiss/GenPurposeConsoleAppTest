using System;
using System.Collections.Generic;
using System.Text;

namespace GenPurpNetCoreTest {
  class GeneralPurposeTests {
    public static void Run() {
      const int hardwareOutput = 0x8000;
      short sh = 0;
      unchecked {
        sh |= (short)hardwareOutput;
      }
      Console.WriteLine($"sh = {sh}");
    }
  }
}
