using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gorillas;
internal class OrbCommRegex {
  public static void Run() {
    string input = "%MGFS: \"FM02.01\",2.1,0,16,3,2,2\"FM03.01\",3.1,0,16,2,2,2OK";
    string regexPatt = "\"FM[0-9]*.[0-9]*\",[0-9]*.[0-9]*,[0-9],[0-9]*,[0-9],[0-9]";
    Regex regex = new Regex(regexPatt);
    MatchCollection matches = regex.Matches(input);
    foreach (Match match in matches) {
      int lastIdx = match.Index + match.Length;
      Console.WriteLine(input[match.Index..lastIdx]);
    }
  }
}
