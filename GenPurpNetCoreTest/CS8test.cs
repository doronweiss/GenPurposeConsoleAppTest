using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPurpNetCoreTest {
  class CS8test {
    public static void
      Run() {
      var words = new string[]
      {
                    // index from start    index from end
        "The",      // 0                   ^9
        "quick",    // 1                   ^8
        "brown",    // 2                   ^7
        "fox",      // 3                   ^6
        "jumped",   // 4                   ^5
        "over",     // 5                   ^4
        "the",      // 6                   ^3
        "lazy",     // 7                   ^2
        "dog"       // 8                   ^1
      };
      Console.WriteLine($"The last word is {words[^1]}");
      Console.WriteLine($"Range length is {words[2..3].Length}");
      var v = words[2..6];
      foreach (string s in v)
        Console.WriteLine($"Word = {s}");
      var rng = 0..10;
      foreach (int i in Enumerable.Range(3,5))
        Console.WriteLine($"Got {i}");

    }
  }
}
