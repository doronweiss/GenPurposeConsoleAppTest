using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenPurpNetCoreTest {
  class AnonJsonTest {
    public static void Run() {
      var x = new {a = 3, b = 3.5, c = "Hello, World"};
      string json = JsonConvert.SerializeObject(x, Formatting.Indented);
      Console.WriteLine(json);
      var b = JsonConvert.DeserializeObject(json);
      Console.WriteLine(b);
      dynamic c = JObject.Parse(json);
      Console.WriteLine(c);
      Console.WriteLine(c.a);
    }
  }
}
