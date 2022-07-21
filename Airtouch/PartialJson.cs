using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Airtouch {
  internal class PartialJson {
    class C2bJasoned {
      [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
      public int? iValue;
      public int? iValue2;
      [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
      public string str = "batata";
    }

    class RobotMessage {
      public int robot_id;
      public Byte[] payload;
      public int len;
    }

    public static string Bytes2String(byte[] bytes) {
      StringBuilder sb = new StringBuilder();
      foreach (byte b in bytes)
        sb.Append(b.ToString("X2"));
      return sb.ToString();
    }

    public static void Run() {
      Console.WriteLine("*** Ignoring null values ***");
      C2bJasoned c2b = new C2bJasoned();
      Console.WriteLine(JsonConvert.SerializeObject(c2b, Formatting.Indented));
      c2b.str = null;
      Console.WriteLine(JsonConvert.SerializeObject(c2b, Formatting.Indented));
      Console.WriteLine("*** serializing bytes[] ***");
      RobotMessage rm = new RobotMessage() { robot_id = 12345, payload = new byte[] { 1, 2, 3, 4, 5 }, len = 5 };
      Console.WriteLine(JsonConvert.SerializeObject(rm, Formatting.Indented));
      var tmpObj = new {
        robot_id = rm.robot_id,
        payload = Bytes2String(rm.payload),
        len = rm.len
      };
      Console.WriteLine(JsonConvert.SerializeObject(tmpObj, Formatting.Indented));
    }
  }
}
