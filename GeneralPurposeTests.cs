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

    private static string Arr2Str(byte[] arr) {
      return string.Join(",", arr.Select(x => x.ToString()));
    }

    public static unsafe void Run() {
      Console.WriteLine("ASCII TESTER");
      foreach (string str in new List<string>() {"hi", "משה", "why"})
        Console.WriteLine($"Is {str} ASCII? - {IsASCII(str)}");
      //bit array
      BitArray ba = new BitArray(BitConverter.GetBytes(347));
      int cs = ba.Length;
      for (int idx = 0; idx < cs; idx++)
        Console.Write($"{(ba[idx] ? "1" : "0")},");
      Console.WriteLine();
      // enum 'is defined'
      for (int idx = 0; idx < 5; idx++) {
        StamEnum se = (StamEnum)idx;
        if (Enum.IsDefined(typeof(StamEnum), se))
          Console.WriteLine($"{idx} => {se}");
        else
          Console.WriteLine($"{idx} not parsable");
      }

      Console.WriteLine("Probability distribution function");
      double cntr = 90.0;
      double std = cntr * 0.07 / 3.0;
      double tmin = cntr - 3.0 * std;
      double tmax = cntr + 3.0 * std;
      double tspan = tmax - tmin;
      Func<double, double> bpdf = x =>
        MathNet.Numerics.SpecialFunctions.Gamma(6) * Math.Pow((x - tmin) * (tmax - x), 2) /
        (MathNet.Numerics.SpecialFunctions.Gamma(3) * MathNet.Numerics.SpecialFunctions.Gamma(3) * Pow(tspan, 5));

      double dx = tspan / 101;
      List<double> pdfs = new List<double>();
      for (int idx = 0; idx <= 100; idx++)
        pdfs.Add(bpdf(tmin + dx * idx));
      Console.WriteLine($"x=[{string.Join(",", pdfs)} ];");

      Console.WriteLine("Array copy");
      byte[] data = new Byte [] {0, 2, 4, 6, 8};
      Console.WriteLine($"data = {Arr2Str(data)}");
      byte[] data2 = data.ToArray();
      Console.WriteLine($"data2 = {Arr2Str(data2)}");
      data[0] = 3;
      data[1] = 4;
      data[2] = 5;
      data[3] = 9;
      data[4] = 11;
      Console.WriteLine($"data = {Arr2Str(data)}");
      Console.WriteLine($"data2 = {Arr2Str(data2)}");

    }
  }
}
