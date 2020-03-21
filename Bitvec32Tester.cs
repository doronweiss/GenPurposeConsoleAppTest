using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class Bitvec32Tester {
    public class SingleRobotStateData {
      public BitVector32 state;
      // *** STATUS access values ***
      public static BitVector32.Section dryrunSec = BitVector32.CreateSection(1);
      public static BitVector32.Section opModeSec = BitVector32.CreateSection(4, dryrunSec);
      public static BitVector32.Section progStateSec = BitVector32.CreateSection(7, opModeSec);
      public static BitVector32.Section progInitPhaseSec = BitVector32.CreateSection(15, progStateSec);
      public static BitVector32.Section progStartFromCurrTheta = BitVector32.CreateSection(1, progInitPhaseSec);
      public static BitVector32.Section weldingSec = BitVector32.CreateSection(1, progStartFromCurrTheta);
      public static BitVector32.Section feederSec = BitVector32.CreateSection(1, weldingSec);
      public static BitVector32.Section weldHaltedSec = BitVector32.CreateSection(1, feederSec);
      public static BitVector32.Section askingOverLimitSec = BitVector32.CreateSection(1, weldHaltedSec);
      // axes status sections
      public static BitVector32.Section thetaEnSec = BitVector32.CreateSection(1, askingOverLimitSec);
      public static BitVector32.Section thetaAbsSec = BitVector32.CreateSection(1, thetaEnSec);
      public static BitVector32.Section yEnSec = BitVector32.CreateSection(1, thetaAbsSec);
      public static BitVector32.Section yAbsSec = BitVector32.CreateSection(1, yEnSec);
      public static BitVector32.Section zEnSec = BitVector32.CreateSection(1, yAbsSec);
      public static BitVector32.Section zAbsSec = BitVector32.CreateSection(1, zEnSec);
    }

    public static void Run() {
      SingleRobotStateData ssd = new SingleRobotStateData();
      Console.WriteLine($"{ssd.state:X}");
      Console.WriteLine($" Offset: {SingleRobotStateData.zAbsSec.Offset}");
    }

  }
}
