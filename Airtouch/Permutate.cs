using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airtouch {
  internal static class Permutator {

    public static IList<IList<int>> Permute(int[] nums) {
      var list = new List<IList<int>>();
      return DoPermute(nums, 0, nums.Length - 1, list);
    }

    static IList<IList<int>> DoPermute(int[] nums, int start, int end, IList<IList<int>> list) {
      if (start == end) {
        // We have one of our possible n! solutions,
        // add it to the list.
        list.Add(new List<int>(nums));
      } else {
        for (var i = start; i <= end; i++) {
          (nums[start], nums[i]) = (nums[i], nums[start]);
          //Swap(ref nums[start], ref nums[i]);
          DoPermute(nums, start + 1, end, list);
          (nums[start], nums[i]) = (nums[i], nums[start]);
          //Swap(ref nums[start], ref nums[i]);
        }
      }
      return list;
    }

    static void Swap(ref int a, ref int b) {
      var temp = a;
      a = b;
      b = temp;
    }

  }

  internal class Permutate {
    // static IEnumerable<IEnumerable<T>>
    //   GetPermutations<T>(IEnumerable<T> list, int length) {
    //   if (length == 1) return list.Select(t => new T[] { t });
    //
    //   return GetPermutations(list, length - 1)
    //     .SelectMany(t => list.Where(e => !t.Contains(e)),
    //       (t1, t2) => t1.Concat(new T[] { t2 }));
    // }

    static void PrintResult(IList<IList<int>> lists) {
      Console.WriteLine("[");
      foreach (var list in lists) {
        Console.WriteLine($"    [{string.Join(',', list)}]");
      }
      Console.WriteLine("]");
    }

    public static void Run() {
      int[] ints = new int[]{ 1, 2, 3};
      IEnumerable<IEnumerable<int>> iperms = Permutator.Permute(ints);
      List<List<int>> perms = new List<List<int>>();
      foreach (IEnumerable<int> ie in iperms)
        Console.WriteLine(string.Join(", ", ie.ToList()));
    }
  }
}
