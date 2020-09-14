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

      // array values conversion
      Console.WriteLine($"array values conversion");
      ushort[] shorties = new ushort[32];
      int [] inties = new int[32];
      foreach (int idx in Enumerable.Range(0, 16)) {
        byte[] bts = BitConverter.GetBytes(idx);
        shorties[idx * 2] = BitConverter.ToUInt16(bts, 0);
        shorties[idx * 2 + 1] = BitConverter.ToUInt16(bts, 2);
      }
      Console.WriteLine($"shorties = {(string.Join("\t", shorties))}");
      Array.Copy(shorties, inties, 32);
      Console.WriteLine($"Array = {(string.Join("\t", inties))}");
      for (int idx = 0; idx < 32; inties[idx++] = 0) ;
      Buffer.BlockCopy(shorties, 0, inties, 0, 64);
      Console.WriteLine($"Buffer = {(string.Join("\t", inties))}");
    }
  }
}
