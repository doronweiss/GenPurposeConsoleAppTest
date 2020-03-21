using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class WebFileExtChange {
    public static void Run() {
      string wfile = "http://www.servotronix.com/html/softMC/libs/configurator_archive.bndl";
      string chngfile = Path.ChangeExtension(wfile, ".md5");
      string cntfile = Path.ChangeExtension(wfile, ".contents");
      Console.WriteLine($"wfile={wfile}");
      Console.WriteLine($"chngfile={chngfile}");
      Console.WriteLine($"cntfile={cntfile}");
    }
  }
}
