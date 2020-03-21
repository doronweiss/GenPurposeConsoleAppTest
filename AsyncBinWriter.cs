using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  public class AsyncBinWriter {

    readonly Queue baseQueue = new Queue();
    readonly Queue synchedQueue = null;
    Thread writerThread = null;
    readonly AutoResetEvent are = new AutoResetEvent(false);
    bool contThread = true;
    readonly string fileName = "";
    BinaryWriter bw = null;

    public AsyncBinWriter(string fileName) {
      this.fileName = fileName;
      synchedQueue = Queue.Synchronized(baseQueue);
    }

    ~AsyncBinWriter() {
      Stop();
    }

    public bool Start(bool append) {
      try {
        bw = new BinaryWriter(File.Open(fileName, FileMode.Create)) {};
      } catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine($"Exception in Start:stop- {ex.Message}");
        return false;
      }
      contThread = true;
      writerThread = new Thread(new ThreadStart(this.WriterThreadFunc));
      //System.Diagnostics.Debug.WriteLine($"{GenUtils.DebugDet(new System.Diagnostics.StackTrace(true).GetFrame(0))} New Thread");
      writerThread.Start();
      return true;
    }

    public void Stop() {
      contThread = false;
      are.Set();
      if (writerThread != null) { // if no connection was made the polling thread is null
        if (!writerThread.Join(250))
          writerThread.Abort();
        writerThread = null;
      }
      try {
        bw?.Close();
        bw?.Dispose();
        bw = null;
      } catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine($"Exception in CSVFile:stop- {ex.Message}");
      }
    }

    public void AddDoubles(double [] data) {
      synchedQueue.Enqueue(data);
      are.Set();
    }

    public void AddIntegers(int [] data) {
      synchedQueue.Enqueue(data);
      are.Set();
    }

    void WriterThreadFunc() {
      while (true) {
        bool res = are.WaitOne(100);
        if (!contThread)
          return;
        if (res) {
          while (synchedQueue.Count > 0) {
            object obj = synchedQueue.Dequeue();
            byte[] arr;
            switch (obj) {
              case double [] doubles:
                arr = doubles.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
                break;
              case int [] integers:
                arr = integers.SelectMany(value => BitConverter.GetBytes(value)).ToArray();
                break;
              default:
                throw  new Exception("Bad data type");
            }
            bw.Write(arr, 0, arr.Length);
          }
        }
      }
    }



  }
}
