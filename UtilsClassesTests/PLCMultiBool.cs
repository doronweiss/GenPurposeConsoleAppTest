namespace UtilsClassesTests {

  public class PLCMultiBool {
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

    public int WhoTrue() =>
      plcBools.FindIndex(x => x == true);
    #endregion index data
  }

  public class PLCMultiBoolE<T> : PLCMultiBool where T : Enum {
    public PLCMultiBoolE(bool initValue) : base(Enum.GetValues(typeof(T)).Length, initValue) { }

    public PLCBoolValues Set(T position, bool value) =>
      Set((int)(ValueType)position, value);

    public (T, bool) WhoRose() {
      int idx = plcBools.FindIndex(x => x.IsRise);
      return  ((T) (ValueType) idx, idx >= 0 );
    }

    public (T, bool) WhoFell() {
      int idx = plcBools.FindIndex(x => x.IsFall);
      return ((T) (ValueType) idx, idx >= 0);
    }

    public (T, bool) WhoTrue() {
      int idx = plcBools.FindIndex(x => x==true);
      return ((T) (ValueType) idx, idx >= 0);
    }

  }
}
