using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SEUtils {
  internal class LogTest {
    public static void Run() {
      var father = new {Logtype = "new order", id = "621232ff4adff200045de5e1", table = 1, burgersNum = 3, burgers = new List<object>()};
      father.burgers.Add(new {
        Sbid = "WAITER", Type = "Sporty", Size = "Reg"
      });
      father.burgers.Add(new {
        Sbid = "WAITER2", Type = "Sporty", Size = "Reg"
      });
      father.burgers.Add(new {
        Sbid = "WAITER3", Type = "Sporty", Size = "Reg"
      });
      Console.WriteLine(JsonConvert.SerializeObject(father));
    }
  }
}
