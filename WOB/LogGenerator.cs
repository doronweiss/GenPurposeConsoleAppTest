using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.CSharp;

namespace GenPurposeConsoleAppTest.WOB {
  [Flags]
  public enum SysOpMode {
    Stopped = 1,  // no operation allowed, system's initial state
    Manual = 2,   // manual - all operations controlled through the TP
    Auto = 4      // runing a plan 
  };

  public enum ProgramStateEnum {
    None, // program not active
    Initializing, // started program, initializing phase
    Running, // program running
    Finalizing,
    Error // encountered an error while running
  };

  public enum ProgInitPhaseEnum {
    PreInit,              // initial state
    WaitForSecondRobot,   // wait for the second robot to move to safe position
    MoveZUp,              // move Z up to avoid hitting the pipe
    MoveZUpWait,          // wait for Z axis to reach up
    MoveToScanStart,      // send MC to start position
    MoveToScanStartWait,  // wait for MC to report reaching start position
    ScanToWeldStart,      // start profiler and send MC to weld start point
    ScanToWeldStartWait,  // wait for MC to report reaching weld start point
    MoveYToWeldStart,     // in AI mode - move Y axis to start position
    MoveYToWeldStartWait, // in AI mode - wait for MC to report Y reached weld start position
    MoveZToWeldStart,     // in AI mode - move Z axis to start position
    MoveZToWeldStartWait, // in AI mode - wait for MC to report Z reached weld start position
    ReadyToWeld           // ready to weld, in AI - start weld, otherwise, wait for another weld start command
  };


  public class GrooveDataPoints { }

  public class SingleRobotStateData {
    public BitVector32 state;
    // *** STATUS access values ***
    private static readonly BitVector32.Section dryrunSec = BitVector32.CreateSection(1);                                               // 0,1
    private static readonly BitVector32.Section opModeSec = BitVector32.CreateSection(4, dryrunSec);                             // 1,3
    private static readonly BitVector32.Section progStateSec = BitVector32.CreateSection(7, opModeSec);                          // 4,3
    private static readonly BitVector32.Section progInitPhaseSec = BitVector32.CreateSection(15, progStateSec);                  // 7,4
    private static readonly BitVector32.Section progStartFromCurrTheta = BitVector32.CreateSection(1, progInitPhaseSec);         // 11,1
    private static readonly BitVector32.Section weldingSec = BitVector32.CreateSection(1, progStartFromCurrTheta);               // 12,1
    private static readonly BitVector32.Section feederSec = BitVector32.CreateSection(1, weldingSec);                            // 13,1
    private static readonly BitVector32.Section weldHaltedSec = BitVector32.CreateSection(1, feederSec);                         // 14,1
    private static readonly BitVector32.Section askingOverLimitSec = BitVector32.CreateSection(1, weldHaltedSec);                // 15,1
    private static readonly BitVector32.Section devModeSec = BitVector32.CreateSection(1, askingOverLimitSec);                   // 16,1
    private static readonly BitVector32.Section prescanSec = BitVector32.CreateSection(1, devModeSec);                           // 17,1
    private static readonly BitVector32.Section weldCommSec = BitVector32.CreateSection(1, prescanSec);                           // 18,1
    // axes status sections
    private static readonly BitVector32.Section thetaEnSec = BitVector32.CreateSection(1, weldCommSec);                           // 19,1
    private static readonly BitVector32.Section thetaAbsSec = BitVector32.CreateSection(1, thetaEnSec);                          // 21,1
    private static readonly BitVector32.Section yEnSec = BitVector32.CreateSection(1, thetaAbsSec);                              // 22,1
    private static readonly BitVector32.Section yAbsSec = BitVector32.CreateSection(1, yEnSec);                                  // 23,1
    private static readonly BitVector32.Section zEnSec = BitVector32.CreateSection(1, yAbsSec);                                  // 24,1
    private static readonly BitVector32.Section zAbsSec = BitVector32.CreateSection(1, zEnSec);                                  // 25,1

    public SingleRobotStateData() {
      dryRun = true;
      currOpmode = SysOpMode.Stopped;
      currProgState = ProgramStateEnum.None;
      currProgInitPhase = ProgInitPhaseEnum.PreInit;
      welding = false;
      inching = false;
      WeldHalted = false;
    }

    #region accessors
    public bool dryRun {
      get { return state[dryrunSec] == 1; }
      set { state[dryrunSec] = value ? 1 : 0; }
    }

    public SysOpMode currOpmode {
      get { return (SysOpMode)state[opModeSec]; }
      set { state[opModeSec] = (int)value; }
    }

    public ProgramStateEnum currProgState {
      get { return (ProgramStateEnum)state[progStateSec]; }
      set { state[progStateSec] = (int)value; }
    }

    public ProgInitPhaseEnum currProgInitPhase {
      get { return (ProgInitPhaseEnum)state[progInitPhaseSec]; }
      set { state[progInitPhaseSec] = (int)value; }
    }

    public bool startFromCurrTheta {
      get { return state[progStartFromCurrTheta] == 1; }
      set { state[progStartFromCurrTheta] = value ? 1 : 0; }
    }

    public bool welding { // weld in process
      get { return state[weldingSec] == 1; }
      set { state[weldingSec] = value ? 1 : 0; }
    }

    public bool inching { // wire feeder is working - "inching" wire
      get { return state[feederSec] == 1; }
      set { state[feederSec] = value ? 1 : 0; }
    }

    public bool WelderCommOk { // wire feeder is working - "inching" wire
      get { return state[weldCommSec] == 1; }
      set { state[weldCommSec] = value ? 1 : 0; }
    }

    public bool WeldHalted {
      get { return state[weldHaltedSec] == 1; }
      set { state[weldHaltedSec] = value ? 1 : 0; }
    }

    public bool PushedOverLimit {
      get { return state[askingOverLimitSec] == 1; }
      set { state[askingOverLimitSec] = value ? 1 : 0; }
    }

    public bool isInDevMode {
      get { return state[devModeSec] == 1; }
      set { state[devModeSec] = value ? 1 : 0; }
    }

    public bool isInPreScan {
      get { return state[prescanSec] == 1; }
      set { state[prescanSec] = value ? 1 : 0; }
    }

    // axes status sections
    public bool ThetaEN { // is Theta enabled
      get { return state[thetaEnSec] == 1; }
      set { state[thetaEnSec] = value ? 1 : 0; }
    }

    public bool ThetaHome { // is Theta homed
      get { return state[thetaAbsSec] == 1; }
      set { state[thetaAbsSec] = value ? 1 : 0; }
    }

    public bool YEN { // is Y enabled
      get { return state[yEnSec] == 1; }
      set { state[yEnSec] = value ? 1 : 0; }
    }

    public bool YHome { // is Y homed
      get { return state[yAbsSec] == 1; }
      set { state[yAbsSec] = value ? 1 : 0; }
    }

    public bool ZEN { // is Z enabled
      get { return state[zEnSec] == 1; }
      set { state[zEnSec] = value ? 1 : 0; }
    }

    public bool ZHome { // is Z homed
      get { return state[zAbsSec] == 1; }
      set { state[zAbsSec] = value ? 1 : 0; }
    }

    public bool Homed {
      get {
        return ThetaHome && YHome && ZHome;
      }
    }

    public bool Enabled {
      get {
        return ThetaEN && YEN && ZEN;
      }
    }
    #endregion accessors

    #region to int and back for transport
    public int ToInt() {
      return state.Data;
    }

    public static SingleRobotStateData FromInt(int data) {
      return new SingleRobotStateData() { state = new BitVector32(data) };
    }
    #endregion to int and back for transport
  }

  public class MCStateData {
    // *** STATUS access values ***
    public static readonly int zAbsuluteMask = BitVector32.CreateMask();                       //0
    public static readonly int yAbsuluteMask = BitVector32.CreateMask(zAbsuluteMask);          //1
    public static readonly int thetaAbsoluteMask = BitVector32.CreateMask(yAbsuluteMask);      //2
    public static readonly int zEnableMask = BitVector32.CreateMask(thetaAbsoluteMask);        //3
    public static readonly int yEnableMask = BitVector32.CreateMask(zEnableMask);              //4
    public static readonly int thetaEnableMask = BitVector32.CreateMask(yEnableMask);          //5
    public static readonly int sysDin101Mask = BitVector32.CreateMask(thetaEnableMask);        //6
    public static readonly int sysDin102Mask = BitVector32.CreateMask(sysDin101Mask);          //7
    public static readonly int sysDin201Mask = BitVector32.CreateMask(sysDin102Mask);          //8
    public static readonly int sysDin202Mask = BitVector32.CreateMask(sysDin201Mask);          //9
    public static readonly int sysDin301Mask = BitVector32.CreateMask(sysDin202Mask);          //10
    public static readonly int sysDin302Mask = BitVector32.CreateMask(sysDin301Mask);          //11
    public static readonly int zIsmovingMask = BitVector32.CreateMask(sysDin302Mask);          //12
    public static readonly int yIsmovingMask = BitVector32.CreateMask(zIsmovingMask);          //13
    public static readonly int thetaIsmovingMask = BitVector32.CreateMask(yIsmovingMask);      //14
    public static readonly int zIssettledMask = BitVector32.CreateMask(thetaIsmovingMask);     //15
    public static readonly int yIssettledMask = BitVector32.CreateMask(zIssettledMask);        //16
    public static readonly int thetaIssettledMask = BitVector32.CreateMask(yIssettledMask);    //17
    public static readonly int zMotionMask = BitVector32.CreateMask(thetaIssettledMask);       //18
    public static readonly int yMotionMask = BitVector32.CreateMask(zMotionMask);              //19
    public static readonly int thetaMotionMask = BitVector32.CreateMask(yMotionMask);          //20
    public static readonly int dummySysMo = BitVector32.CreateMask(thetaMotionMask);           //21
    public static readonly int sysEnMask = BitVector32.CreateMask(dummySysMo);                 //22
    public static readonly int isOscillatingMask = BitVector32.CreateMask(sysEnMask);          //23
    public static readonly int res1 = BitVector32.CreateMask(isOscillatingMask);               //24
    public static readonly int res2 = BitVector32.CreateMask(res1);                            //25
    public static readonly int res3 = BitVector32.CreateMask(res2);                            //26
    public static readonly int res4 = BitVector32.CreateMask(res3);                            //27
    public static readonly int res5 = BitVector32.CreateMask(res4);                            //28
    public static readonly int res6 = BitVector32.CreateMask(res5);                            //29
    public static readonly int res7 = BitVector32.CreateMask(res6);                            //30
    public static readonly int PLCInError = BitVector32.CreateMask(res7);                      //31
    // *** FLAGS access values ***
    public static readonly int TP1 = BitVector32.CreateMask();               // 0 - first TP selector bit
    public static readonly int TP2 = BitVector32.CreateMask(TP1);            //1  - second TP selector bit
    public static readonly int ESTOP = BitVector32.CreateMask(TP2);          //2  - TP ESTOP button
    public static readonly int dummy03 = BitVector32.CreateMask(ESTOP);      //3 
    public static readonly int dummy04 = BitVector32.CreateMask(dummy03);    //4
    public static readonly int dummy05 = BitVector32.CreateMask(dummy04);    //5
    public static readonly int dummy06 = BitVector32.CreateMask(dummy05);    //6
    public static readonly int dummy07 = BitVector32.CreateMask(dummy06);    //7
    public static readonly int dummy08 = BitVector32.CreateMask(dummy07);    //8
    public static readonly int dummy09 = BitVector32.CreateMask(dummy08);    //9
    public static readonly int dummy10 = BitVector32.CreateMask(dummy09);    //10
    public static readonly int dummy11 = BitVector32.CreateMask(dummy10);    //11
    public static readonly int dummy12 = BitVector32.CreateMask(dummy11);    //12
    public static readonly int dummy13 = BitVector32.CreateMask(dummy12);    //13
    public static readonly int dummy14 = BitVector32.CreateMask(dummy13);    //14
    public static readonly int dummy15 = BitVector32.CreateMask(dummy14);    //15
    public static readonly int dummy16 = BitVector32.CreateMask(dummy15);    //16
    public static readonly int dummy17 = BitVector32.CreateMask(dummy16);    //17
    public static readonly int dummy18 = BitVector32.CreateMask(dummy17);    //18
    public static readonly int dummy19 = BitVector32.CreateMask(dummy18);    //19
    public static readonly int dummy20 = BitVector32.CreateMask(dummy19);    //20
    public static readonly int dummy21 = BitVector32.CreateMask(dummy20);    //21
    public static readonly int dummy22 = BitVector32.CreateMask(dummy21);    //22
    public static readonly int dummy23 = BitVector32.CreateMask(dummy22);    //23
    public static readonly int dummy24 = BitVector32.CreateMask(dummy23);    //24
    public static readonly int dummy25 = BitVector32.CreateMask(dummy24);    //25
    public static readonly int dummy26 = BitVector32.CreateMask(dummy25);    //26
    public static readonly int dummy27 = BitVector32.CreateMask(dummy26);    //27
    public static readonly int dummy28 = BitVector32.CreateMask(dummy27);    //28
    public static readonly int dummy29 = BitVector32.CreateMask(dummy28);    //29
    public static readonly int dummy30 = BitVector32.CreateMask(dummy29);    //30
    public static readonly int dummy31 = BitVector32.CreateMask(dummy30);    //31

    public bool flagsUpdated = false; // mark that new flags/MC state data was read
    public double thetaPos, thetaVel, Ypos, Yvel, Zpos, Zvel;
    public int mcClock;
    public BitVector32 mcStatus, mcFlags, mcError, mcErrorAux;
    public int extra;
    // Data added when shifting to omron
    public bool plcAck, plcDone, plcFail;
    //public double volt, amp, inclinometer; - provisions for future implementation
    // flags
    public bool thetaHomed => mcStatus[thetaAbsoluteMask];

    SysOpMode prevOpMode = SysOpMode.Stopped;
    public SysOpMode TPRequestedOpMode {
      get {
        SysOpMode? newOpMode = null;
        if (mcFlags[TP1] && !mcFlags[TP2])
          newOpMode = SysOpMode.Stopped;
        else if (mcFlags[TP1] && mcFlags[TP2])
          newOpMode = SysOpMode.Manual;
        else if (!mcFlags[TP1] && mcFlags[TP2])
          newOpMode = SysOpMode.Auto;
        else
          newOpMode = SysOpMode.Stopped;
        if (newOpMode != prevOpMode)
          prevOpMode = (SysOpMode)newOpMode;
        return (SysOpMode)newOpMode;
      }
    }

    public bool TPEStop => mcFlags[ESTOP];

    public void CopyTo(MCStateData other) {
      // first check changes and set flags
      other.flagsUpdated = flagsUpdated;
      // now copy data
      other.thetaPos = thetaPos;
      other.thetaVel = thetaVel;
      other.Ypos = Ypos;
      other.Yvel = Yvel;
      other.Zpos = Zpos;
      other.Zvel = Zvel;
      other.mcClock = mcClock;
      other.mcError = mcError;
      other.mcErrorAux = mcErrorAux;
      other.mcStatus = mcStatus;
      other.mcFlags = mcFlags;
      other.extra = extra;
      // omron additions
      other.plcAck = plcAck;
      other.plcDone = plcDone;
      other.plcFail = plcFail;
      //other.volt = volt;
      //other.amp = amp;
      //other.inclinometer = inclinometer;
      // mark current as used
      flagsUpdated = false;
    }
  }



  public class RobotCycleData {
    // state data
    public SingleRobotStateData robState = new SingleRobotStateData();
    // motion
    public MCStateData mcData = new MCStateData();
    public double oscillationCenter, oscillationAmplitude; // inserted by notion, not MC
    public double stickout;
    // profile
    public GrooveDataPoints grooveData = null;
    // *add actual profile
    // V/A data
    public double measVolt, measCurrent;
    // inclinometer
    public double currInclination;
    // welder
    public double voltageFB, currentFB, wireSpeedFB;

    public delegate double FeederRPM2MPMConvertor(double rpm);
    public static FeederRPM2MPMConvertor FeederRPM2MPM;

    public static string Title() {
      return
        "RSD, ThetaP[Deg], ThetaV[cm/Min], YP[mm], ZP[mm], Inclination[Deg], grv UN, grv BN, grv UP, grv BP, V fb[V], Meas V[V], A fb[A], Meas A[A], Wire speed fb [M/Min], Osc Width[mm], Osc Center[mm]";
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("");
      sb.Append($"{LogGenerator.dt.ToString("HH:mm:ss.fff")},RSD, {mcData.thetaPos:F3},{(mcData.thetaVel * 60 / 10):F3}, {mcData.Ypos:F3}, {mcData.Zpos:F3}, {currInclination:F3},");
      sb.Append("0.0/0.0,0.0/0.0,0.0/0.0,0.0/0.0,");
      sb.Append($"{voltageFB:F3},{ measVolt:F3},{currentFB:F3},{measCurrent:F3},{FeederRPM2MPM(wireSpeedFB):F3}, {oscillationAmplitude:F2}, {oscillationCenter:F3}");
      return sb.ToString();
    }
  }

  class LogGenerator {
    const double wheelDiameterMM = 19.36; 
    const double wheelDiameterM = wheelDiameterMM / 1000.0;
    const double gearRatio = 5.3*5;//33.0;
    const double Int16Scale = 32768.0;

    public static double RPM2MPMConvertor(double RpM){
      return (RpM/gearRatio)*Math.PI*wheelDiameterM;
    }

    static public DateTime dt = DateTime.Now;
    public static void Run() {
      Random rand = new Random();
      RobotCycleData rsd = new RobotCycleData();
      RobotCycleData.FeederRPM2MPM = RPM2MPMConvertor;
      using (StreamWriter sw = new StreamWriter("mygenlog.csv")) {
        for (int idx = 0; idx < 1000; idx++) {
          dt = dt.AddMilliseconds(10);
          rsd.mcData.thetaPos = idx;
          rsd.mcData.thetaVel = rand.NextDouble()/10.0 + 0.5;
          rsd.mcData.Ypos = rand.NextDouble() + 30.0;
          rsd.mcData.Zpos = rand.NextDouble() - 30.0;
          rsd.currInclination = rsd.mcData.thetaVel - 0.5 + rand.NextDouble();
          rsd.voltageFB = Math.Sin((idx / 10.0));
          rsd.measVolt = rsd.voltageFB + rand.NextDouble() / 10.0;
          rsd.currentFB = Math.Cos((idx / 10.0));
          rsd.measCurrent = rsd.currentFB + rand.NextDouble() / 10.0;
          rsd.wireSpeedFB = rand.NextDouble() + 5.0;
          rsd.oscillationAmplitude = rand.NextDouble();
          rsd.oscillationCenter = rsd.mcData.Ypos + rand.NextDouble();
          sw.WriteLine(rsd);
          if (idx % 50 == 0) {
            StringBuilder sb = new StringBuilder("");
            sb.Append(
              $"{dt.ToString("HH:mm:ss.fff")},NSD, {rsd.mcData.thetaPos:F3},{1.12}, {rsd.mcData.Ypos:F3}, {rsd.mcData.Zpos:F3}, {rsd.currInclination:F3},");
            sb.Append(
              $" {Math.Sin((idx / 10.0)):F3},{Math.Cos((idx / 10.0)):F3},{5.0+Math.Sin(idx/20.0)*0.5:F3}," +
              $"0.3");
            sw.WriteLine(sb.ToString());
          }
        }
      }
    }
  }
}
