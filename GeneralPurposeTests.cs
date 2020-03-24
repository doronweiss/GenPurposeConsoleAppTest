using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using static System.Math;

namespace GenPurposeConsoleAppTest {
  class GeneralPurposeTests {
    [Flags]
    enum StamEnum {
      aa = 1,
      bb = 2,
      cc = 4,
      dd = 8
    };

    private static string FmtDT(DateTime dt) {
      return $"{dt.Year:D4}/{dt.Month:D2}/{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}:{dt.Second:D2}";
    }

    public static bool IsASCII(string str) {
      return Encoding.UTF8.GetByteCount(str) == str.Length;
    }

    public static void Run() {
      //      const int arraySize = 20;
      //      int* fib = stackalloc int[arraySize];
      //      int* p = fib;
      //      // The sequence begins with 1, 1.
      //      *p++ = *p++ = 1;
      //      for (int i = 2; i < arraySize; ++i, ++p) {
      //        // Sum the previous two numbers.
      //        *p = p[-1] + p[-2];
      //      }
      //      for (int i = 0; i < arraySize; ++i) {
      //        Console.WriteLine(fib[i]);
      //      }
      //
      /*
            DateTime dt = DateTime.Now;
            Console.WriteLine ($"Now is - { FmtDT(dt)}");
            dt = dt.AddDays(154);
            Console.WriteLine($"Future is - { FmtDT(dt)}");
      */
      /*
            foreach (string str in new List<string>(){"hi", "משה", "why"})
              Console.WriteLine( $"Is {str} ASCII? - {IsASCII(str)}");
      */
      /*
            BitArray ba = new BitArray(BitConverter.GetBytes(347));
            int cs = ba.Length;
            for (int idx=0; idx<cs; idx++)
              Console.Write($"{(ba[idx]?"1":"0")},");
            Console.WriteLine();
      */
      //double pi2 = 2.0 * Math.PI;
      //Console.WriteLine($"6.5555");
      //Console.WriteLine($"{6.5555 % pi2}");

      //      for (int idx = 0; idx < 5; idx++) {
      //        StamEnum se = (StamEnum) idx;
      //        if (Enum.IsDefined(typeof(StamEnum) , se))
      //          Console.WriteLine($"{idx} => {se}");
      //        else
      //          Console.WriteLine($"{idx} not parsable");
      //      }

      //      double cntr = 90.0;
      //      double std = cntr * 0.07 / 3.0;
      //      double tmin = cntr - 3.0 * std;
      //      double tmax = cntr + 3.0 * std;
      //      double tspan = tmax - tmin;
      //
      //      Func<double, double> bpdf = x =>
      //        MathNet.Numerics.SpecialFunctions.Gamma(6) * Math.Pow((x - tmin) * (tmax - x), 2) /
      //        (MathNet.Numerics.SpecialFunctions.Gamma(3) * MathNet.Numerics.SpecialFunctions.Gamma(3) * Pow(tspan, 5));
      //
      //      double dx = tspan / 101;
      //      List<double> pdfs=new List<double>();
      //      for (int idx=0; idx<=100; idx++)
      //        pdfs.Add(bpdf(tmin+dx*idx));
      //      Console.WriteLine($"x=[{string.Join(",", pdfs)} ];");
      double x = Math.PI;
      Console.WriteLine($"{x}, {x:F3}, {x:F5}, {x:F15}");
    }
  }
}
