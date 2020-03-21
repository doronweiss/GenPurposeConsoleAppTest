using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class TasksTest {
    [DllImport("kernel32.dll")]
    static extern uint GetCurrentThreadId();

    private static List<Task> tasks = new List<Task>();

    public static void Run() {
      for (int idx = 0; idx < 1; idx++) {
        Task t = new Task(TaskWorker);
        tasks.Add(t);
        t.Start();
      }
      Task.WaitAll(tasks.ToArray());
    }

    static void TaskWorker() {
      uint sum = 0;
      while (sum < uint.MaxValue)
        sum = sum + 1;
    }
  }
}
