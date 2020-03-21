using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class RegexXMLSplitter {
    public static void Run() {
      string xmlContent = "<Index>0x607A</Index><SubIndex>0</SubIndex><BitLen>32</BitLen>";
      string pattern = "(<.*?>)|(.+?(?=<|$))";
      MatchCollection mtchs = Regex.Matches(xmlContent, pattern);
      foreach (Match mtch in mtchs)
        Console.WriteLine(mtch.Value);
    }
  }
}
