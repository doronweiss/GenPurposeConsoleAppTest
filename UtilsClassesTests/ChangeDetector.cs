using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsClassesTests {
  internal class PLCMultiBool16 {
    const int dataSize=16;
    private bool changed = false;
    private BitVector16 bv = new BitVector16(0);
    private static readonly ushort[] masks = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768 };
    private bool[] changes = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

    public void Set(int idx, bool value) {
      if (idx < 0 || idx >= dataSize)
        return;
      bool currValue = bv[masks[idx]];
      changes[idx] = currValue != value;
      bv[masks[idx]] = value;
    }

    public bool Changed => changes.Any(x => x);

    public static implicit operator bool(PLCMultiBool16 b) => b.bv.Data!=0;

  }
}
