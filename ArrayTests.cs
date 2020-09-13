using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class ArrayTests {
    private static string Arr2Str(int [] arr) => string.Join(",", arr);

    public static void Run() {
      // reverse an array
      // double[] arr = new double[] {1.0, 2.0, 4.0, 8.0, 16.0, 32.0, 64.0, 128.0, 256.0, 512.0, 1024.0, 2048.0, 4096.0, 8192.0};
      // int sz = arr.Length;
      // int rsz = sz / 2;
      // string str = string.Join(",", arr);
      // Console.WriteLine($"Before: {str}");
      // for (int idx = 0; idx < rsz; idx++) {
      //   double t = arr[idx];
      //   arr[idx] = arr[sz - 1 - idx];
      //   arr[sz - 1 - idx] = t;
      // }
      // str = string.Join(",", arr);
      // Console.WriteLine($"After: {str}");

      int [] src = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
      Console.WriteLine($"Source = {Arr2Str(src)}");
      int[] dest1 = src;
      Console.WriteLine($"dest1 = {Arr2Str(dest1)}");
      int[] dest2 = src.ToArray();
      Console.WriteLine($"dest2 = {Arr2Str(dest2)}");
      src[0] = 8;
      Console.WriteLine($"Modified");
      Console.WriteLine($"Source = {Arr2Str(src)}");
      Console.WriteLine($"dest1 = {Arr2Str(dest1)}");
      Console.WriteLine($"dest2 = {Arr2Str(dest2)}");
    }
  }
}
