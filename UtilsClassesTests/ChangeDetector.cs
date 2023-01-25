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

  internal class PLCMultiBool {
    private int dataSize = 16;
    protected List<PLCBool> plcBools = default;

    public PLCMultiBool(int dataSize, bool initValue) {
      this.dataSize = dataSize;
      plcBools = new List<PLCBool>();
      for (int idx = 0; idx < dataSize; idx++)
        plcBools.Add(new PLCBool(initValue));
    }

    public PLCBoolValues Set(int idx, bool value) {
      if (idx < 0 || idx >= dataSize)
        return PLCBoolValues.False;
      return plcBools[idx].Set(value);
    }

    public void Reset() => 
      plcBools.ForEach(x => x.Reset());

    public bool IsRise => plcBools.Any(x => x.IsRise);

    public bool IsFall => plcBools.Any(x => x.IsFall);

    public bool Changed => IsRise || IsFall;

    public static implicit operator bool(PLCMultiBool b) => 
      b.plcBools.Any(x => (bool)x);

    #region index data
    public int WhoRose() =>
      plcBools.FindIndex(x => x.IsRise);

    public int WhoFell() =>
      plcBools.FindIndex(x => x.IsFall);
    #endregion index data
  }

  internal class PLCMultiBoolE<T> : PLCMultiBool where T : Enum {
    public PLCMultiBoolE(bool initValue) : base(Enum.GetValues(typeof(T)).Length, initValue) { }

    public PLCBoolValues Set(T position, bool value) =>
      Set((int)(ValueType)position, value);

    public T WhoRose() =>
      (T)(ValueType)plcBools.FindIndex(x => x.IsRise);

    public T WhoFell() =>
      (T)(ValueType)plcBools.FindIndex(x => x.IsFall);


  }
}
