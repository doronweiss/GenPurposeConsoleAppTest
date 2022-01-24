using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GenPurpNetCoreTest {
  // time an operation
  public class OpTimer {
    private DateTime opStartDT;

    public OpTimer() {
      opStartDT = DateTime.UtcNow;
    }

    public void Reset() {
      opStartDT = DateTime.UtcNow;
    }

    public double Elapsed => (DateTime.UtcNow - opStartDT).TotalMilliseconds;

  }
  internal class DynaClassBuilder {
    public static void Run() {
      dynamic sampleObject = new ExpandoObject();
      sampleObject.TestProperty = "Dynamic Property"; // Setting dynamic property.
      Console.WriteLine(sampleObject.TestProperty);
      Console.WriteLine(sampleObject.TestProperty.GetType());
// This code example produces the following output:
// Dynamic Property
// System.String
      const int lim = 1000000;
      dynamic test = new ExpandoObject();
      OpTimer ot = new OpTimer();
      for (int idx = 0; idx < lim; idx++) {
        ((IDictionary<string, object>)test).Add("DynamicProperty", 5);
        ((IDictionary<string, object>)test).Remove("DynamicProperty");
      }
      double el = ot.Elapsed;
      Console.WriteLine($"Avg op time = {el/lim} [S]");
      ((IDictionary<string, object>)test).Add("DynamicProperty", 5);
      ((IDictionary<string, object>)test).Add("DynamicProperty2", "doron");
      ((IDictionary<string, object>)test).Add("DynamicProperty3", new OpTimer());
      Console.WriteLine(JsonConvert.SerializeObject(test, Formatting.Indented));
    }
  }
}
