using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airtouch {
  internal class CommandBuildTest {

    private static (byte[], int) CmdToBuffer(byte [] command, params object [] list ) {
      List<byte> result = new List<byte>();
      result.AddRange(command);
      // foreach (object par in list)
      //   result.AddRange(BitConverter.GetBytes(par));
      return (result.ToArray(), result.Count);
    }

    public static void Run() {
      byte[] cmd = new byte[]{ (byte)0x1, (byte)0x3 };
      (byte[] buff, int len) = CmdToBuffer(cmd, 7, 8, 1.2345);
    }
  }
}
