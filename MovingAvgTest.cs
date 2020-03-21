using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSScriptLibrary;

namespace GenPurposeConsoleAppTest {
  public class PointLoc {
    public double x;
    public double z;

    public PointLoc(double z, double x) {
      this.x = x;
      this.z = z;
    }

    public PointLoc() { }

    public override string ToString() {
      return $"{x}/{z}";
    }
  }


  public class MovingAverage {
    object lockObj = new object();
    private int cycleLen = 0, cycleWidth = 0;
    private int cycleIdx = 0;
    private double[,] data;
    private double[] sums;
    private int count = 0;

    public MovingAverage(int windowLen, int windowWidth) {
      cycleLen = windowLen;
      cycleWidth = windowWidth;
      data = new double[cycleWidth, cycleLen];
      sums = new double[cycleWidth];
      Reset();
    }

    // zero everything
    private void Reset() {
      cycleIdx = 0;
      count = 0;
      foreach (int idx in Enumerable.Range(0, cycleWidth)) {
        sums[idx] = 0.0;
        foreach (int jdx in Enumerable.Range(0, cycleWidth)) {
          data[idx, jdx] = 0.0;
        }
      }
    }

    public void Add(double value) {
      if (cycleWidth != 1)
        throw new Exception("Bad implementation");
      lock (lockObj) {
        sums[0] -= data[0, cycleIdx];
        data[0, cycleIdx] = value;
        sums[0] += data[0, cycleIdx];
        cycleIdx = (cycleIdx + 1) % cycleLen;
        count = count < cycleLen ? count + 1 : count;
      }
    }

    public void Add(int value) {
      Add((double)value);
    }

    public void Add(PointLoc point) {
      if (cycleWidth != 2)
        throw new Exception("Bad implementation");
      lock (lockObj) {
        sums[0] -= data[0, cycleIdx];
        sums[1] -= data[1, cycleIdx];
        data[0, cycleIdx] = point.x;
        data[1, cycleIdx] = point.z;
        sums[0] += data[0, cycleIdx];
        sums[1] += data[1, cycleIdx];
        cycleIdx = (cycleIdx + 1) % cycleLen;
        count = count < cycleLen ? count + 1 : count;
      }
    }

    public double ValueDouble() {
      if (cycleWidth != 1)
        throw new Exception("Bad implementation");
      lock (lockObj) {
        if (count == 0)
          return 0.0;
        return sums[0] / count;
      }
    }

    public double ValueInt() {
      return (int)ValueDouble();
    }

    public PointLoc ValuePoint() {
      if (cycleWidth != 2)
        throw new Exception("Bad implementation");
      lock (lockObj) {
        if (count == 0)
          return new PointLoc(0.0, 0.0);
        return new PointLoc(sums[0] / count, sums[1] / count);
      }
    }

  }

  class MovingAvgTest {
    public static void Run() {
      Random random = new Random();  
      MovingAverage mv1 = new MovingAverage(10, 1);
      MovingAverage mv2 = new MovingAverage(10, 2);
      using (StreamWriter sw = new StreamWriter("mavgs.txt")) {
        for (int idx = 0; idx <= 10000; idx++) {
          double r1 = random.Next(0, 10000) / 100000.0;
          //        double r2x = r1;//random.Next(0, 10000) / 10000.0;
          //        double r2y = r1 * 2.0; //random.Next(0, 10000) / 10000.0;

          r1 = Math.Sin(idx / 1000.0) + r1;
          double r2x = r1;//random.Next(0, 10000) / 10000.0;
          double r2y = r1 * 2.0; //random.Next(0, 10000) / 10000.0;
          mv1.Add(r1);
          mv2.Add(new PointLoc(r2x, r2y));
          if (idx > 0 && idx % 100 == 0)
            sw.WriteLine($"{r1},{mv1.ValueDouble()},  {mv2.ValuePoint().x},  {mv2.ValuePoint().z}");
            //sw.WriteLine($"Scalar avg: {mv1.ValueDouble()}, Point: {mv2.ValuePoint()}");
        }
      }
    }
  }
}
