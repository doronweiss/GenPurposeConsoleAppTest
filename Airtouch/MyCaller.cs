using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airtouch {
  internal class MyCaller {
    private static int counter = 0;
    private static void func1() {
      counter++;
      StackTrace stackTrace = new StackTrace();
      Console.WriteLine($"In {stackTrace.GetFrame(0).GetMethod().Name}, called from: {stackTrace.GetFrame(1).GetMethod().Name}");
      switch (counter) {
        case <=1:
          func2();
          break;
        case 2:
          func1();
          break;
        case >= 3:
          return;
      }
    }

    private static void func2() {
      func1();
    }


    private static void JustPrint() {
      //Console.WriteLine($"My name is inigo montoya, ");
      summer += 1;
    }

    private static void JustPrintST() {
      StackTrace stackTrace = new StackTrace();
      //Console.WriteLine($"My name is inigo montoya, st={stackTrace.GetFrame(0).GetMethod().Name}");
      summer += 1;
    }

    private static string GetLogPoint() {
      StackTrace stackTrace = new StackTrace();
      StackFrame sf = stackTrace.GetFrame(1);
      return $"[{sf.GetMethod().DeclaringType.ToString().Replace(".","][")}][{sf.GetMethod().Name}]";
    }

    private static string TesterFunc() {
      return GetLogPoint();
    }

    private static long summer = 0;
    public static void Run() {
      int loops = 1000000;
      // Stopwatch st = new Stopwatch();
      // Console.WriteLine("Starting");
      // st.Start();
      // for (int idx=0; idx<loops; idx++)
      //   JustPrint();
      // st.Stop();
      // long jpt = st.ElapsedMilliseconds;
      // Console.WriteLine($"Summer = {summer}");
      // summer = 0;
      // st.Reset();
      // st.Start();
      // for (int idx=0; idx<loops; idx++)
      //   JustPrintST();
      // st.Stop();
      // Console.WriteLine($"Summer = {summer}");
      // long stpt = st.ElapsedMilliseconds;
      // Console.WriteLine($"Elapssed: {jpt} / {stpt}");
      // Console.WriteLine($"Single op: {jpt * 1000.0 / loops} / {stpt * 1000.0 / loops} [micro sec]");

      Stopwatch st = new Stopwatch();
      Console.WriteLine("Starting");
      st.Start();
      string str="";
      for (int idx=0; idx<loops; idx++)
        str = TesterFunc();
      st.Stop();
      long jpt = st.ElapsedMilliseconds;
      Console.WriteLine($"str = {str}");
      Console.WriteLine($"Elapssed: {jpt}");
      Console.WriteLine($"Single op: {jpt * 1000.0 / loops} [micro sec]");

      //func1();
    }
  }
}
