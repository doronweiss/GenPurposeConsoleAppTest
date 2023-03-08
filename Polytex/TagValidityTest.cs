using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polytex {
  internal class TagValidityTest {
    static bool IsLegalChar(char ch, List<char> allowedChars) {
      return true;
    }

    static bool CheckTag(string tag) {
      List<char> allowedChars = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F' };
      char[] tagChars = tag.ToCharArray();
      return tagChars.Length >= 10 && tagChars.All(x => allowedChars.Contains(x));
    }

    public static void Run() {
      string tag = "0123456789";
      Console.WriteLine($"tag: {tag} returned: {CheckTag(tag)}");
      tag = "01234567";
      Console.WriteLine($"tag: {tag} returned: {CheckTag(tag)}");
      tag = "0123456789abcdef";
      Console.WriteLine($"tag: {tag} returned: {CheckTag(tag)}");
      tag = "0123456789ABCDEF";
      Console.WriteLine($"tag: {tag} returned: {CheckTag(tag)}");
      tag = "0123456789ABCDEFW";
      Console.WriteLine($"tag: {tag} returned: {CheckTag(tag)}");
    }
  }
}
