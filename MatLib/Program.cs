using System;

namespace MatLib {
  class Program {
    static void Main(string[] args) {
      DMatrix m1 = new DMatrix(2, 3, 2.5);
      Console.WriteLine(m1);
      DMatrix m2 = new DMatrix(3,2,1.0);
      Console.WriteLine(m2);
      Console.WriteLine(m1*m2);
      DMatrix m3 = new DMatrix(2, 3, 1.5);
      Console.WriteLine(m1.D * m3);
    }
  }
}
