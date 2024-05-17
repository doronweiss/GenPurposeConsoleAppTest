using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mitras2024 {
  internal class FlexJSON {
    public static void Run() {
      string json = File.ReadAllText("c:\\Users\\User\\OneDrive\\Projects\\Aerodan\\mitrasdata.json");
      Newtonsoft.Json.Linq.JObject jsonObj = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);
      Console.WriteLine(jsonObj["vehicle"]["capabilities"]);
    }
  }
}
