using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://stackoverflow.com/questions/2256048/store-a-reference-to-a-value-type

namespace Airtouch {
  internal class EnumReffer {
    enum AnEnum {
      A,
      B,
      C
    };

    class TgtClass {
      public AnEnum en;
    }

    class EnRefferal<T>  { 
      T locRef;

      // public EnRefferal(T remote) {
      //   locRef = &remote;
      // }
    }


    public static void Run() { }
  }
}
