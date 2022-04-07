using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gorillas {
  internal class Base64Test {

    public static string Bytes2HexString(byte[] data) {
      StringBuilder sb = new StringBuilder();
      data.ToList().ForEach(x => sb.Append(x.ToString("X2")));
      return sb.ToString();
    }
    public static void Run() {
      string str = "AAAAAAAAAAAA7O/nKeRiHcyRtUAI/oM=";
      byte[] data = Convert.FromBase64String(str);
      Console.WriteLine("Req:");
      Console.WriteLine($"Total of {data.Length} bytes");
      Console.WriteLine(Bytes2HexString(data));
      System.Diagnostics.Debug.WriteLine(Bytes2HexString(data));
      str = "ICO2BUTZ8xCbUf11nr8j5puakdu4lgHSKk9CXsYtst3R";
      data = Convert.FromBase64String(str);
      Console.WriteLine("Resp:");
      Console.WriteLine($"Total of {data.Length} bytes");
      Console.WriteLine(Bytes2HexString(data));
      System.Diagnostics.Debug.WriteLine(Bytes2HexString(data));
    }
  }
}
