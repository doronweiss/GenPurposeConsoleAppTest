using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class AsyncAwaitTest {
    static async void Runme() {
      Task<int> access = Task.Factory.StartNew(DoSomethingAsync);
      // task independent stuff here

      // this line is reached after the 5 seconds sleep from 
      // DoSomethingAsync() method. Shouldn't it be reached immediately? 
      int a = await access;
      Console.WriteLine($"result={a}");

      // from my understanding the waiting should be done here.
      //int x = await access;
    }

    static int DoSomethingAsync() {
      // is this executed on a background thread?
      int sum = 0;
      for (int idx = 0; idx < 5; idx++) {
        sum += idx;
        Console.WriteLine($"long running, idx={idx}");
        System.Threading.Thread.Sleep(1000);
      }
      return sum;
    }

    public static void Run() {
      Runme();
      for (int idx = 0; idx < 25; idx++) {
        Console.WriteLine($"Main, idx={idx}");
        System.Threading.Thread.Sleep(250);
      }
    }

  }
}
