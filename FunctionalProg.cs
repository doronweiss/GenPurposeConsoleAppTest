using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  public static class IEnumExtensions {
    public static List<Tuple<T,T>> Pairwise<T>(this IEnumerable<T> src) {
      return src.Zip(src.Skip(1), Tuple.Create).ToList();
    }
  }

  class FunctionalProg {
    static int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

    static int EvenOrZero(int x) {
      return x % 2 == 0 ? x : 0;
    }

    static bool isAscending(List<int> numbers) {
      var p = numbers.Pairwise().Select(x => Math.Abs(x.Item1 - x.Item2)).ToList().Pairwise().Select(x => x.Item2 - x.Item1).ToList();
      return ! p.Any(x => x >= 0);
    }

    public static void Run() {
      //      int sv1 = numbers.Sum((Func<int, int>)EvenOrZero);
      //      Console.WriteLine($"Sum V1 = {sv1}");
      //      int sv2 = (from s in numbers where s % 2 == 0 select s).Sum();
      //      Console.WriteLine($"Sum V2 = {sv2}");
      bool res = isAscending(new List<int>() { 2,4,6 });
      Console.WriteLine(res);
      res = isAscending(new List<int>() { 5,3,2 });
      Console.WriteLine(res);
    }
  }
}
