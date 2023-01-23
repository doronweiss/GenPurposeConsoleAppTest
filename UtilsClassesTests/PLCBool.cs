namespace UtilsClassesTests
{
  public enum PLCBoolValues { Fall, False, Rise, True };
  public class PLCBool
  {
    readonly PLCBoolValues[,] transitionMat = new PLCBoolValues[4, 2] {
      //              got false              got true
  /* was fall */    {PLCBoolValues.False, PLCBoolValues.Rise},
  /* was false */   {PLCBoolValues.False, PLCBoolValues.Rise},
  /* was rise */    {PLCBoolValues.Fall, PLCBoolValues.True},
  /* was true */    {PLCBoolValues.Fall, PLCBoolValues.True}
  };
    private PLCBoolValues _value;
    private PLCBoolValues _originalValue;

    public PLCBool()
    {
      _value = _originalValue = PLCBoolValues.False;
    }

    public PLCBool(bool initValue)
    {
      _originalValue = initValue ? PLCBoolValues.True : PLCBoolValues.False;
      _value = _originalValue;
    }

    public PLCBoolValues Set(bool bValue)
    {
      int prev = (int)_value;
      int curr = bValue ? 1 : 0;
      _value = transitionMat[prev, curr];
      return _value;
    }

    public void Reset() => _value = _originalValue;

    public PLCBoolValues Value => _value;

    public bool IsRise => _value == PLCBoolValues.Rise;

    public bool IsFall => _value == PLCBoolValues.Fall;

    public bool Changed => IsRise || IsFall;

    public static implicit operator PLCBoolValues(PLCBool b) => b._value;

    public static implicit operator bool(PLCBool b) => b._value == PLCBoolValues.True || b._value == PLCBoolValues.Rise;
  }

  public class LimitAlerter
  {
    public enum AlerterState
    {
      Ok,
      Alert,
      Blocked
    };
    private int limit;
    private bool alertCalled;

    public LimitAlerter(int limit)
    {
      alertCalled = false;
      this.limit = limit;
    }

    // public AlerterState IsAlert(int value) => 
    //  value < limit ? AlerterState.Ok : (value == limit ? AlerterState.Alert : AlerterState.Blocked);

    public bool IsAlert(int value)
    {
      if (value == limit && !alertCalled)
      {
        alertCalled = true;
        return true;
      }
      else
        return false;
    }

    public void Reset() => alertCalled = false;
  }

  public class PLCBoolVector
  {
    readonly PLCBoolValues[,] transitionMat = new PLCBoolValues[4, 2] {
      {PLCBoolValues.False, PLCBoolValues.Rise},
      {PLCBoolValues.False, PLCBoolValues.Rise},
      {PLCBoolValues.Fall, PLCBoolValues.True},
      {PLCBoolValues.Fall, PLCBoolValues.True}
    };
    private int sizeOfValues = 32;
    private PLCBoolValues[] _values;

    public PLCBoolVector(int size, bool initValue = false)
    {
      sizeOfValues = size;
      _values = new PLCBoolValues[sizeOfValues];
      for (int idx = 0; idx < sizeOfValues; idx++)
        _values[idx] = initValue ? PLCBoolValues.True : PLCBoolValues.False;
    }

    public PLCBoolValues[] Set(int bValues)
    {
      for (int idx = 0; idx < sizeOfValues; idx++)
      {
        int prev = (int)_values[idx];
        int curr = (bValues & 0x1) == 0x1 ? 1 : 0;
        _values[idx] = transitionMat[prev, curr];
        bValues = bValues >> 1;
      }
      return _values;
    }

    public PLCBoolValues Value(int idx)
    {
      return _values[idx];
    }

    public bool Changed(int idx)
    {
      return _values[idx] == PLCBoolValues.Rise || _values[idx] == PLCBoolValues.Fall;
    }
  }
}