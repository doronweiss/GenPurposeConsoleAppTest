using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GenPurposeConsoleAppTest.WOB;
using MathNet.Numerics.Random;

namespace GenPurposeConsoleAppTest {
  class Bitvec32Tester {

    [StructLayout(LayoutKind.Explicit)]
    struct UBV32 {
      // *** STATUS access values ***
      public static readonly int m1 = BitVector32.CreateMask(); //0
      public static readonly int m2 = BitVector32.CreateMask(m1); //1
      public static readonly int m3 = BitVector32.CreateMask(m2); //2
      public static readonly int m4 = BitVector32.CreateMask(m3); //3
      
      [FieldOffset(0)]
      public BitVector32 state;
      [FieldOffset(0)]
      public int data;
    }

    public static void Run() {
      UBV32 ssd = new UBV32();
      ssd.data = 9;
      Console.WriteLine($"{ssd.data:X}");
      Console.WriteLine($" Offset: {ssd.state[UBV32.m1]}, {ssd.state[UBV32.m2]}, {ssd.state[UBV32.m3]}, {ssd.state[UBV32.m4]}");
      ssd.data = 1;
      Console.WriteLine($"{ssd.data:X}");
      Console.WriteLine($" Offset: {ssd.state[UBV32.m1]}, {ssd.state[UBV32.m2]}, {ssd.state[UBV32.m3]}, {ssd.state[UBV32.m4]}");
      ssd.data = 6;
      Console.WriteLine($"{ssd.data:X}");
      Console.WriteLine($" Offset: {ssd.state[UBV32.m1]}, {ssd.state[UBV32.m2]}, {ssd.state[UBV32.m3]}, {ssd.state[UBV32.m4]}");
      ssd.data = 14;
      Console.WriteLine($"{ssd.data:X}");
      Console.WriteLine($" Offset: {ssd.state[UBV32.m1]}, {ssd.state[UBV32.m2]}, {ssd.state[UBV32.m3]}, {ssd.state[UBV32.m4]}");
    }

  }
}
