using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class DestructorTest {

    class Dummy {
      public int num;

      ~Dummy() {
        Console.WriteLine($"Num = {num}");
      }
    }

    public static void Run() {
      Dummy d1 = new Dummy() {num = 3};
      Dummy d2 = new Dummy() {num = 34};
      Dummy d3 = new Dummy() {num = 345};
      Dummy d4 = new Dummy() {num = 3456};
      Dummy d5 = new Dummy() {num = 34567};
    }
  }
}
