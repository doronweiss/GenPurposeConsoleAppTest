using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class CRCTester {
    public static void Run() {
      string str = "123456789";
      byte[] bts = Encoding.ASCII.GetBytes(str);
      ushort crc = CRC16.calc_crc16(bts, 0, (ushort)str.Length);
      string crcs = crc.ToString("X");
      Console.WriteLine($"{str} => {crcs}");
    }
  }
}
