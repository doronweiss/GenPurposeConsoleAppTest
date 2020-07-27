using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GenPurposeConsoleAppTest {
  enum Rainbow {
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Violet
  }

  class CS8Test {
    static int FromRainbow(Rainbow colorBand) =>
    colorBand switch
    {
      Rainbow.Red => 0,
      Rainbow.Orange => 1,
      Rainbow.Yellow => 2,
      Rainbow.Green => 3,
      Rainbow.Blue => 4,
      Rainbow.Indigo => 5,
      Rainbow.Violet => 6,
      _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
    };

    static int Cmpr (int a, int b)=>
      (a,b) switch{
        (0,0) =>0,
        (1,0) => 1,
        (0,1)=>-1,
        var(x,y) when x==y => 1000,
        (_,_) =>-999
      };
    
    public static void Run() {
      Rainbow r = Rainbow.Orange;
      Console.WriteLine($"Color: {r} => {FromRainbow(r)}");
      int a=1, b=0;
      Console.WriteLine($"values: {a}/{b} => {Cmpr(a,b)}");
      a = 0;b = 1;
      Console.WriteLine($"values: {a}/{b} => {Cmpr(a,b)}");
      a = 0; b = 0;
      Console.WriteLine($"values: {a}/{b} => {Cmpr(a,b)}");
      a = 2; b = 2;
      Console.WriteLine($"values: {a}/{b} => {Cmpr(a, b)}");
      a = 2; b = 153;
      Console.WriteLine($"values: {a}/{b} => {Cmpr(a, b)}");
    }
  }
}
