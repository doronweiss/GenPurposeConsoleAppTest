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

    static void YetAnother() {
      int[] a = new int[5] { 10, 20, 30, 40, 50 };
      // Must be in unsafe code to use interior pointers.
      unsafe {
        // Must pin object on heap so that it doesn't move while using interior pointers.
        fixed (int* p = &a[0]) {
          // p is pinned as well as object, so create another pointer to show incrementing it.
          int* p2 = p;
          Console.WriteLine(*p2);
          // Incrementing p2 bumps the pointer by four bytes due to its type ...
          p2 += 1;
          Console.WriteLine(*p2);
          p2 += 1;
          Console.WriteLine(*p2);
          Console.WriteLine("--------");
          Console.WriteLine(*p);
          // Dereferencing p and incrementing changes the value of a[0] ...
          *p += 1;
          Console.WriteLine(*p);
          *p += 1;
          Console.WriteLine(*p);
        }
      }
    }

      public static void Run() {
      TestFunc("batata");
      int[] tarr = new[] {1, 2};
      Console.WriteLine($"Array before: {tarr[0]}, {tarr[1]}");
      swap(tarr);
      Console.WriteLine($"Array after: {tarr[0]}, {tarr[1]}");
      FiboExample();
      YetAnother();
    }
  }
}
