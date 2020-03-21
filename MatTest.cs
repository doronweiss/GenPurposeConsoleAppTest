using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class matrix3 {
    double[] data = new double[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public double Get(int i, int j) {
      return data[i * 3 + j];
    }

    public void Set(int i, int j, double val) {
      data[i * 3 + j] = val;
    }

    public static  matrix3 operator *(matrix3  me, matrix3 other) {
      matrix3 res = new matrix3();
      for (int idx = 0; idx < 3; idx++) {
        for (int jdx = 0; jdx < 3; jdx++) {
          double s = 0.0;
          for (int kdx = 0; kdx < 3; kdx++) {
            s += me.Get(idx, kdx) * other.Get(kdx, jdx);
          }
          res.Set(idx, jdx, s);
        }
      }
      return res;
    }

  }

  class MatTest {
    public static void Run() {
      int count = 1000000;
      matrix3 a = new matrix3();
      matrix3 b = new matrix3();
      matrix3 c;
      Stopwatch sw = new Stopwatch();
      sw.Start();
      for (int i = 0; i < count; i++)
        c = a * b;
      sw.Stop();
      Console.WriteLine($"{count} loops => {sw.ElapsedMilliseconds} [MS], Division took: {sw.ElapsedMilliseconds/count} [MS]");
    }
  }
}
