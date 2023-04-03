using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Mono.CSharp;

namespace GenPurposeConsoleAppTest {
  class UnionTest {
    [StructLayout(LayoutKind.Explicit, Size=16)]
    public unsafe struct MyUnion {
      [FieldOffset(0)]public int i1;
      [FieldOffset(4)]public int i2;
      [FieldOffset(8)]public int i3;
      [FieldOffset(12)]public int i4;
      [FieldOffset(0)]public fixed byte bts[16];

      public string PrintBytes() {
        string str = $"{bts[0]:X2},{bts[1]:X2},{bts[2]:X2},{bts[3]:X2},{bts[4]:X2},{bts[5]:X2},{bts[6]:X2},{bts[7]:X2},{bts[8]:X2},{bts[9]:X2},{bts[10]:X2},{bts[11]:X2},{bts[12]:X2},{bts[13]:X2},{bts[14]:X2},{bts[15]:X2}";
        return str;
      }
    }

    public static string Bytes2String(byte[] bytes, bool isBigEndian) {
      byte[] workBytes = !isBigEndian ? bytes : bytes.Reverse().ToArray();
      return BitConverter.ToString(workBytes).Replace("-", "");
    }

    public static unsafe string PrintBytes(MyUnion mu) {
      string str = $"{mu.bts[0]:X2},{mu.bts[1]:X2},{mu.bts[2]:X2},{mu.bts[3]:X2},{mu.bts[4]:X2},{mu.bts[5]:X2},{mu.bts[6]:X2},{mu.bts[7]:X2},{mu.bts[8]:X2},{mu.bts[9]:X2},{mu.bts[10]:X2},{mu.bts[11]:X2},{mu.bts[12]:X2},{mu.bts[13]:X2},{mu.bts[14]:X2},{mu.bts[15]:X2}";
      return str;
    }

    public static void Run() {
      MyUnion mu;
      mu.i1 = 1;
      mu.i2 = 2;
      mu.i3 = 3;
      mu.i4 = 4;
      Console.WriteLine($"Ints: {mu.i1}, {mu.i2}, {mu.i3}, {mu.i4}");
      Console.WriteLine(PrintBytes(mu));
      Console.WriteLine(Bytes2String(mu.bts, false));
    }
  }
}
