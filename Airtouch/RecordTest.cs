using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airtouch {
  record MyRec(string name, int age);

  internal class RecordTest {
    public static void Run() {
      var mr1 = new MyRec("doron", 62);
      Console.WriteLine($"mr1= {mr1}");
      var mr2 =  mr1 with{age = 50};
      Console.WriteLine($"mr2= {mr2}");
    }
  }
}
