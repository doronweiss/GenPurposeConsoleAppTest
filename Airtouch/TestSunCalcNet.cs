using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SunCalcNet;
using SunCalcNet.Model;

namespace Airtouch {
  internal class TestSunCalcNet {
    public static void Run() {
      double R2D = 180.0 / Math.PI;
      //DateTime dt = DateTime.Now.Date.AddHours(13.0).ToUniversalTime();
      DateTime dt = DateTime.Now.ToUniversalTime();
      //var date = DateTime.Now.ToUniversalTime();
      var lat = 32.18002;
      var lng = 34.93658;
      int loops = 10000;
      SunPosition pos = new();
      Stopwatch sw = new Stopwatch();
      sw.Start();
      for (int idx=0; idx<loops; idx++) 
        pos = SunCalc.GetSunPosition(dt, lat, lng);
      sw.Stop();
      long millis = sw.ElapsedMilliseconds;
      Console.WriteLine($"Sun pos: {180+pos.Azimuth*R2D}/{pos.Altitude*R2D}");
      Console.WriteLine($"Took: {millis} [ms], loops: {loops}, per calc: {millis * 1.0 / loops} [ms]");
    }
  }
}
