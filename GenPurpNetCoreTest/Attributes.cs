using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  internal class Attributes {
    class MyClass {
      public int ival { get; set; } = 3;
      [DisplayName("somename")]
      public string myName { get; set; } = "doron";
    }

    public static void Run() {
      Console.WriteLine("Starting");
      PropertyInfo pi = typeof(MyClass).GetProperty("myName");
      DisplayNameAttribute pInfo = pi.GetCustomAttribute<DisplayNameAttribute>();
      var name = pInfo.DisplayName;
      Console.WriteLine($"Name: {name}");
      //
      pi = typeof(MyClass).GetProperty("myName");
      DisplayNameAttribute attr =
        (DisplayNameAttribute) Attribute.GetCustomAttribute(pi, typeof(DisplayNameAttribute));
      string str = $" myname: {attr?.DisplayName ?? "None"}";
      Console.WriteLine(str);
      pi = typeof(MyClass).GetProperty("myName");
      List<Attribute> attrs = Attribute.GetCustomAttributes(pi).ToList();
      if (attrs.Any(x => x.GetType() == typeof(DisplayNameAttribute))) {
        DisplayNameAttribute dna = (DisplayNameAttribute)attrs.First(x => x.GetType() == typeof(DisplayNameAttribute));
        str = $" ival: {dna.DisplayName ?? "None"}";
        Console.WriteLine(str);
      } else
        Console.WriteLine("no display name");
    }

  }
}
