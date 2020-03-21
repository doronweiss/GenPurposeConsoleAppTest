using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/

namespace GenPurposeConsoleAppTest {
  class ASyncTest {
    private static Timer tmr;

    async static Task<string> GetWebpageLength(string url) {
      HttpClient client = new HttpClient();
      Task<string> getStringTask= client.GetStringAsync(url);
      string urlContents = await getStringTask;
      if (string.IsNullOrEmpty(urlContents))
        return "Null";
      else {
        int mn = (int) Math.Min(1000, urlContents.Length);
        return urlContents.Substring(0, mn);
      }
    }

    private static int count = 0;
    public static void Run() {
      tmr = new Timer();
      tmr.Interval = 50;
      tmr.Elapsed += Tmr_Elapsed;
      tmr.Enabled = true;
      Task<string> ts = GetWebpageLength("http://www.google.com");
      //Task<string> ts = GetWebpageLength("http://www.10fs.co.il");
      //Console.WriteLine($"First 10 chars = {ts.Result}");
      string res;
      try {
        res = ts.Result;
      } catch {
        res = "None";
      }
      Console.WriteLine($"First 10 chars = {res}");
      tmr.Enabled = false;
    }

    private static void Tmr_Elapsed(object sender, ElapsedEventArgs e) {
      Console.WriteLine($"Now: {++count}");
    }
  }
}
