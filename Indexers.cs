using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenPurposeConsoleAppTest;

namespace GenPurposeConsoleAppTest {
  class Idxs {
    public double this[int i] => Math.Sin(i*1.0);

    public string Me() {
      return "Batata";
    }
  }
}

  class Indexers {
    public static void Run(){
      Idxs iidd = new Idxs();
      for (int idx = 0; idx < 25; idx++)
        Console.WriteLine($"{iidd[idx]}");
      Console.WriteLine(iidd.Me());
    }
}
