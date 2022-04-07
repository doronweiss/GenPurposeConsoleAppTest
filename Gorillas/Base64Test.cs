using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gorillas {
  internal class Base64Test {
    static char[] HexChars = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

    public static string Bytes2HexString(byte[] data) {
      StringBuilder sb = new StringBuilder();
      foreach (byte bt in data) {
        int low = bt & 0xf;
        int high = (bt & 0xf0) >> 4;
        sb.Append(HexChars[low]);
        sb.Append(HexChars[high]);
      }
      return sb.ToString();
    }
    public static void Run() {
      string str = "AAAAAAAAAAAAEKrYeyUnT6gX+R/BsW8=";
      byte[] data = Convert.FromBase64String(str);
      Console.WriteLine("Req:");
      Console.WriteLine($"Total of {data.Length} bytes");
      Console.WriteLine(Bytes2HexString(data));
      str = "ICO2BUTZ8xCbUf11nr8j5puakdu4lgHSKk9CXsYtst3R";
      data = Convert.FromBase64String(str);
      Console.WriteLine("Resp:");
      Console.WriteLine($"Total of {data.Length} bytes");
      Console.WriteLine(Bytes2HexString(data));
    }
  }
}
