using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class FeederPositions {
    private const int feederPortionMoveFwd = 20;
    private const int feederPortionMoveBack = 12;
    public static int FeederPos2Portions(int feederPos) {
      if (feederPos < feederPortionMoveFwd)
        return 0;
      else
        return 1 + FeederPos2Portions(
          feederPos - (feederPortionMoveFwd - feederPortionMoveBack)
        );
    }

    public static void Run() {
      int leftPos = 57;
      Console.WriteLine($"Portions for {leftPos} = {FeederPos2Portions(leftPos)}");
    }
  }
}
