using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class UnsafeTest {
    static unsafe void TestFunc(string text) {
      int length = text.Length;
      fixed (char* value = text) {
        char* ptr = value;
        while (*ptr != '\0') {
          Console.Write(*ptr);
          ++ptr;
        }
      }
      Console.WriteLine();
    }

    static public unsafe void swap(int [] pq)  
    {
      fixed (int * fpq = pq) {
        int temp = fpq[0];
        fpq[0] = fpq[1];
        fpq[1] = temp;
      }
    }

    static unsafe void FiboExample() {
      const int arraySize = 20;
      int* fib = stackalloc int[arraySize];
      int* p = fib;
      // The sequence begins with 1, 1.
      *p++ = *p++ = 1;
      for (int i = 2; i < arraySize; ++i, ++p) {
        // Sum the previous two numbers.
        *p = p[-1] + p[-2];
      }
      for (int i = 0; i < arraySize; ++i) {
        Console.WriteLine(fib[i]);
      }
    }

    public static void Run() {
      TestFunc("batata");
      int[] tarr = new[] {1, 2};
      Console.WriteLine($"Array before: {tarr[0]}, {tarr[1]}");
      swap(tarr);
      Console.WriteLine($"Array after: {tarr[0]}, {tarr[1]}");
      FiboExample();
    }
  }
}
