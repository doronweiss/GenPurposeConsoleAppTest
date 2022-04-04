using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GenPurpNetCoreTest {
  internal class MultiJSONTest {
    public class ClsA {
      public int aa=1;
      public int ba=2;
      public string ca = "Batata";
    }
    public class ClsC {
      public int ac=3;
      public int bc=4;
      public string cc = "Catata";
    }

    public static void Run() {
      StringBuilder sb = new StringBuilder("");
      sb.Append(JsonConvert.SerializeObject(new ClsA()));
      sb.Append(JsonConvert.SerializeObject(new ClsC()));
      string str = sb.ToString();
      Console.WriteLine($"JSON: {str}");
      ClsA a = JsonConvert.DeserializeObject<ClsA>(str);
      Console.WriteLine($"A: {JsonConvert.SerializeObject(a)}");
      ClsC c = JsonConvert.DeserializeObject<ClsC>(str);
      Console.WriteLine($"C: {JsonConvert.SerializeObject(c)}");
    }
  }
}
