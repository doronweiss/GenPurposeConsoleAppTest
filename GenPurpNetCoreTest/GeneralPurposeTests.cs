using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GenPurpNetCoreTest {
  class GeneralPurposeTests {
    static void TestUnchecked() {
      const int hardwareOutput = 0x8000;
      short sh = 0;
      unchecked {
        sh |= (short)hardwareOutput;
      }
      Console.WriteLine($"sh = {sh}");
    }

    static void TestDriveInfo() {
      DriveInfo[] allDrives = DriveInfo.GetDrives();

      foreach (DriveInfo d in allDrives) {
        Console.WriteLine("Drive {0}", d.Name);
        Console.WriteLine("  Drive type: {0}", d.DriveType);
        if (d.IsReady == true) {
          Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
          Console.WriteLine("  File system: {0}", d.DriveFormat);
          Console.WriteLine(
            "  Available space to current user:{0, 15} bytes",
            d.AvailableFreeSpace);
          Console.WriteLine(
            "  Total available space:          {0, 15} bytes",
            d.TotalFreeSpace);
          Console.WriteLine(
            "  Total size of drive:            {0, 15} bytes ",
            d.TotalSize);
        }
      }
    }

    static void TestListComprehansion() {
      IEnumerable<int> e = Enumerable.Range(0, 10);
      List<int> l = (from x in e select x*x).ToList();
      l.ForEach((x) => { Console.WriteLine(x); });
    }

    public static void Run() {
      //TestUnchecked();
      //TestDriveInfo();
      TestListComprehansion();
    }
  }
}