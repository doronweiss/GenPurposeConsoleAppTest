using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airtouch {

  class Person {
    public string name;
    public int age;

    public override string ToString() => 
      $"{name}/{age}";
  }

  internal class Permutator<T> {

    public static IList<IList<T>> Permute(IList<T> nums) {
      var list = new List<IList<T>>();
      return DoPermute(nums, 0, nums.Count - 1, list);
    }

    static IList<IList<T>> DoPermute(IList<T> nums, int start, int end, IList<IList<T>> list) {
      if (start == end) {
        // We have one of our possible n! solutions,
        // add it to the list.
        list.Add(new List<T>(nums));
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

    static void Swap(ref T a, ref T b) {
      var temp = a;
      a = b;
      b = temp;
    }

  }

  internal class Permutate {
    static void PrintResult(IList<IList<int>> lists) {
      Console.WriteLine("[");
      foreach (var list in lists) {
        Console.WriteLine($"    [{string.Join(',', list)}]");
      }
      Console.WriteLine("]");
    }

    public static void Run() {
      Person[] ps = new Person[] { 
        new Person() { name = "doron", age = 63 }, new Person() { name = "jenny", age = 61 }, new Person() { name = "snoopy", age= 11 } };
      IEnumerable<IEnumerable<Person>> iperms = Permutator<Person>.Permute(ps);
      List<List<int>> perms = new List<List<int>>();
      foreach (IEnumerable<Person> ie in iperms)
        Console.WriteLine(string.Join(", ", ie.ToList()));
    }
  }
}
