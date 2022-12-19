using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Airtouch {
  internal class JsonDerived {
    public abstract class MyBase {
      public int numBase = 0;
      public abstract void DoSomething();
    }

    public class Deriv1 : MyBase {
      public int num1D1 = 1;

      public override void DoSomething() {
        numBase += num1D1;
      }
    }

    public class Deriv2 : MyBase {
      public int num1D2 = 1;
      public int num2D2 = 3;

      public override void DoSomething() {
        numBase += num1D2 + num2D2;
      }
    }

    public class Container {
      public List<MyBase> objects = new List<MyBase>() {new Deriv1(), new Deriv1(), new Deriv2()};
    }

    public static void Run() {
      Container cnt = new Container();
      JsonSerializerSettings settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
      string str = JsonConvert.SerializeObject(cnt, settings);
      Console.WriteLine("*******************************************************");
      Console.WriteLine(str);
      Console.WriteLine("*******************************************************");
      Container cnt2 = JsonConvert.DeserializeObject<Container>(str, settings);
      Console.WriteLine(JsonConvert.SerializeObject(cnt2));
      Console.WriteLine("*******************************************************");
      Console.WriteLine(JsonConvert.SerializeObject(cnt2, settings));
      Console.WriteLine("*******************************************************");
    }
  }
}
