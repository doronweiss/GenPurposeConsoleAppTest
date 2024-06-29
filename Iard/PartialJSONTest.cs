using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Iard {
  public class BoundingBoxEx {
    public List<int> bb { get; set; }
    public double confidence { get; set; }
    public string label { get; set; }
  }

  public class Tiles {
    public List<List<int>> grid { get; set; }
    public List<BoundingBoxEx> bb { get; set; }
  }

  public class Merged {
    public List<BoundingBoxEx> bb { get; set; }
  }

  internal class PartialJSONTest {
    public static void Run() {
      Merged merged = new Merged();
      merged.bb = new List<BoundingBoxEx>() { new BoundingBoxEx() { bb = new List<int>() { 1, 2, 3, 4 }, confidence = 0.5, label = "sdsds" } };
      string json = Newtonsoft.Json.JsonConvert.SerializeObject(merged);
      Tiles tls = JsonConvert.DeserializeObject<Tiles>(json);
      Console.WriteLine(JsonConvert.SerializeObject(tls));
    }
  }
}
