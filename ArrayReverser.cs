using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class ArrayReverser {
    public static void Run() {
      double[] arr = new double[] {1.0, 2.0, 4.0, 8.0, 16.0, 32.0, 64.0, 128.0, 256.0, 512.0, 1024.0, 2048.0, 4096.0, 8192.0};
      int sz = arr.Length;
      int rsz = sz / 2;
      string str = string.Join(",", arr);
      Console.WriteLine($"Before: {str}");
      for (int idx = 0; idx < rsz; idx++) {
        double t = arr[idx];
        arr[idx] = arr[sz - 1 - idx];
        arr[sz - 1 - idx] = t;
      }
      str = string.Join(",", arr);
      Console.WriteLine($"After: {str}");
    }
  }
}
