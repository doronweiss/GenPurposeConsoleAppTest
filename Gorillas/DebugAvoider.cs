using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://dunnhq.com/posts/2022/debugger-attributes/

namespace Gorillas {
  internal class DebugAvoider {
    //[DebuggerStepThrough()]
    [DebuggerHidden]
    private static void Write2Lines() {
      Console.WriteLine("Line 2");
      Console.WriteLine("Line 3");
    }

    public static void Run() { 
      Console.WriteLine("Line 1");
      Write2Lines();
      Console.WriteLine("Line 4");
    }
  }
}
