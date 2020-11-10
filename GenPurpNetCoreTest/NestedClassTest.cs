using System;
using System.Collections.Generic;
using System.Text;

namespace GenPurpNetCoreTest {
  class NestedClassTest {
    class OuterClass {
      private int privateI;
      InnerClass ic;

      public OuterClass() {
        ic = new InnerClass();
        ic.oc = this;
      }

      public int ISetter {
        get => ic.GetPI();
        set => privateI = value;
      }

      public class InnerClass {
        public OuterClass oc;
        public int GetPI() {
          return oc?.privateI ?? 0;
        }
      }
    }

    public static void Run() { 
      OuterClass oc = new OuterClass();
      oc.ISetter = 12;
      Console.WriteLine($"Result = {oc.ISetter}");
    }
  }
}
