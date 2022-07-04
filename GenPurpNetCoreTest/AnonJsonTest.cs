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
      OpTimer opt = new OpTimer();
      var b = JsonConvert.DeserializeObject(json);
      Console.WriteLine(b);
      long sum = 0;
      int loops = 1000000;
      for (int idx = 0; idx < loops; idx++) {
        dynamic c = JObject.Parse(json);
        sum = sum + (int)c.a;
        // Console.WriteLine($"c={c}");
        // Console.WriteLine($"c.a={c.a}/{c.a.Type}");
        // Console.WriteLine($"c.b={c.b}/{c.b.Type}");
        // Console.WriteLine($"c.c={c.c}/{c.c.Type}");
      }
      Console.WriteLine($"Elapsed: {opt.Elapsed}, sum={sum}, single op: {opt.Elapsed * 1.0 / loops}");
    }
  }
}
