using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class NumericIntegTest {
    //Quick Demo of Numerical integration
    public static void Run() {
      //Show results for different numbers of trapezoids
      for (int i = 4; i <= 100; i+=4)
        Console.WriteLine($"{i}:  =>{TrapezRule(x => x * x, 1, 2, i)}");
      //Change x * x above to numerically integrate
      //a different function than f(x) = x^2
      //Above integrates between x=1 and x=2
    }

    static double TrapezRule(Func<double, double> func, double a, double b, int n) {
      double area = 0;
      double w = (b - a) / n;
      area += (func(a) + func(b)) / 2;
      for (int p = 1; p < n; ++p)
        area += func(a + w * p);
      return w * area;
    }
  }
}
