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

    public static void Run() {
      C2bJasoned c2b = new C2bJasoned();
      Console.WriteLine(JsonConvert.SerializeObject(c2b, Formatting.Indented));
      c2b.str = null;
      Console.WriteLine(JsonConvert.SerializeObject(c2b, Formatting.Indented));
    }
  }
}
