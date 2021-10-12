using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class MyClass {
    private string StackToStr(System.Diagnostics.StackTrace t) {
      StringBuilder sb = new StringBuilder("");
      StackFrame[] frames = t.GetFrames();
      int sz = frames.Length;
      for (int idx = 0; idx < sz; idx++)
        sb.AppendLine($"[{idx}] {frames[idx].GetMethod().Name} / {frames[idx].GetFileLineNumber()}");
      return sb.ToString();
    }


    public void E() {
      System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
      Console.WriteLine(StackToStr(t));
      Console.WriteLine("*******************");
      Console.WriteLine(Environment.StackTrace);
    }
    public void D() { E(); ; }
    public void C() { D(); }
    public void B() { C(); }
    public void A() { B(); }
  }

  class StackPrinter {
    public static void Run() {
      MyClass mc = new MyClass();
      mc.A();
    }
  }
}
