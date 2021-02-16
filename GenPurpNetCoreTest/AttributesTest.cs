using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  public enum SomeEnum { e1, e2, e3, e4 };

  [AttributeUsage(AttributeTargets.Field)]
  public class JSOptionsAttribute : Attribute {
    private string _attrValue;

    public string attrValue => _attrValue;

    public JSOptionsAttribute(string attrValue) {
      this._attrValue = attrValue;
    }
  }

  static class GenUtils {
    public static string Enum2JSON<T>() where T : System.Enum {
      var result = new Dictionary<int, string>();
      List<string> parts = new List<string>();
      var values = Enum.GetValues(typeof(T));
      foreach (int item in values)
        parts.Add($"{{\"key\": \"{Enum.GetName(typeof(T), item)}\",\"value\": \"{item}\"}}, ");
      StringBuilder sb = new StringBuilder();
      sb.Append("[");
      sb.Append(string.Join(",", parts));
      sb.Append("]");
      return sb.ToString();
    }
  }

  class DataRec {
    public int f1;
    [JSOptionsAttribute("batata")]
    public int f2;
    public int f3;
  }

  class AttributesTest {
    public static void Run() {
      FieldInfo[] fields = typeof(DataRec).GetFields(BindingFlags.Public | BindingFlags.Instance);
      foreach (var field in fields) {
        Console.WriteLine(field.Name);
        JSOptionsAttribute attr =
          (JSOptionsAttribute)Attribute.GetCustomAttribute(field, typeof(JSOptionsAttribute));
        if (attr != null)
          Console.WriteLine($"Attr: {attr.attrValue}");
      }
      Console.WriteLine(GenUtils.Enum2JSON<SomeEnum>());
    }
  }
}
