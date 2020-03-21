using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class SignChangeTest {
    public static void Run() {
      int i32 = -12345;
      byte[] bts = BitConverter.GetBytes(i32);
      UInt32 ui = BitConverter.ToUInt32(bts, 0);
      int val;
      // if ((ui & 0x80000000) != 0)
      //   val = -(int)(0xfffffffff - ui);
      // else
      //   val = (int)ui;
      bts = BitConverter.GetBytes(ui);
      val = BitConverter.ToInt32(bts, 0);
      Console.WriteLine($"Start with: {i32}, negated to: {ui}, ended with: {val}");

    }
  }
}
