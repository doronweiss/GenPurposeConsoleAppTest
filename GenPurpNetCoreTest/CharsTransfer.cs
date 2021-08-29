using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class CharsTransfer {
    private static string CharsToHex(char[] chars) {
      StringBuilder sb = new StringBuilder();
      foreach (char ch in chars)
        sb.Append($"{(int)ch:X2}");
      return sb.ToString();
    }

    //private static char[] HexToChars(string str);
    public static char[] HexToChars(string hex) {
      if (hex.Length % 2 != 0)
        return null;
      try {
        return Enumerable.Range(0, hex.Length)
          .Where(x => x % 2 == 0)
          .Select(x => (char)Convert.ToByte(hex.Substring(x, 2), 16))
          .ToArray();
      } catch {
        return null;
      }
    }


    public static void Run() {
      char[] ignoreChars = new char[] { '\n','\t','\r' };
      Console.WriteLine(CharsToHex(ignoreChars));
      char[] ignoChars2 = HexToChars(CharsToHex(ignoreChars));
      char[] ignoChars3 = HexToChars(CharsToHex(ignoreChars)+"s");
    }
  }
}
