using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  class BitVectors {
    public static int zAbsulute = BitVector32.CreateMask();
    public static int yAbsulute = BitVector32.CreateMask(zAbsulute);
    public static int thetaAbsolute = BitVector32.CreateMask(yAbsulute);
    public static int zEnable = BitVector32.CreateMask(thetaAbsolute);
    public static  int yEnable = BitVector32.CreateMask(zEnable);
    public static  int thetaEnable = BitVector32.CreateMask(yEnable);
    public static  int sysDin101 = BitVector32.CreateMask(thetaEnable);
    public static  int sysDin102 = BitVector32.CreateMask(sysDin101);
    public static  int sysDin201 = BitVector32.CreateMask(sysDin102);
    public static  int sysDin202 = BitVector32.CreateMask(sysDin201);
    public static  int sysDin301 = BitVector32.CreateMask(sysDin202);
    public static  int sysDin302 = BitVector32.CreateMask(sysDin301);
    public static  int zIsmoving = BitVector32.CreateMask(sysDin302);
    public static  int yIsmoving = BitVector32.CreateMask(zIsmoving);
    public static  int thetaIsmoving = BitVector32.CreateMask(yIsmoving);
    public static  int zIssettled = BitVector32.CreateMask(thetaIsmoving);
    public static  int yIssettled = BitVector32.CreateMask(zIssettled);
    public static  int thetaIssettled = BitVector32.CreateMask(yIssettled);
    public static  int zMotion = BitVector32.CreateMask(thetaIssettled);
    public static  int yMotion = BitVector32.CreateMask(zMotion);
    public static  int thetaMotion = BitVector32.CreateMask(yMotion);

    public static void Run() { 
      BitVector32 bv = new BitVector32(17);
      List<int> masks = new List<int>() {
        zAbsulute, yAbsulute, thetaAbsolute, zEnable, yEnable, thetaEnable, sysDin101, sysDin102,
        sysDin201, sysDin202, sysDin301, sysDin302, zIsmoving, yIsmoving, thetaIsmoving, zIssettled,
        yIssettled, thetaIssettled, zMotion, yMotion, thetaMotion};
      foreach (int i in masks)
        Console.WriteLine($"Mask={i}, Value={bv[i]}");
    }
  }
}
