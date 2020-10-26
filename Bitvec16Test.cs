using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {

  public struct BitVector16 {
    private ushort data;

    /// <devdoc>
    /// <para>Initializes a new instance of the BitVector16 structure with the specified internal data.</para>
    /// </devdoc>
    public BitVector16(short data) {
      this.data = (ushort)data;
    }

    /// <devdoc>
    /// <para>Initializes a new instance of the BitVector16 structure with the information in the specified 
    ///    value.</para>
    /// </devdoc>
    public BitVector16(BitVector16 value) {
      this.data = value.data;
    }

    /// <devdoc>
    ///    <para>Gets or sets a value indicating whether all the specified bits are set.</para>
    /// </devdoc>
    public bool this[int bit] {
      get {
        return (data & bit) == (uint)bit;
      }
      set {
        if (value) {
          data = (ushort)(data | (uint)bit);
        } else {
          data = (ushort)(data & ~(uint)bit);
        }
      }
    }

    /// <devdoc>
    ///    <para>Gets or sets the value for the specified section.</para>
    /// </devdoc>
    public int this[Section section] {
      get {
        return (int)((data & (uint)(section.Mask << section.Offset)) >> section.Offset);
      }
      set {
#if DEBUG
        if ((value & section.Mask) != value) {
          Debug.Fail("Value out of bounds on BitVector16 Section Set!");
        }
#endif
        value <<= section.Offset;
        int offsetMask = (0xFF & (int)section.Mask) << section.Offset;
        data = (ushort)(int)((data & ~(uint)offsetMask) | ((uint)value & (uint)offsetMask));
      }
    }

    /// <devdoc>
    ///    returns the raw data stored in this bit vector...
    /// </devdoc>
    public int Data {
      get {
        return (int)data;
      }
    }

    private static short CountBitsSet(short mask) {

      // yes, I know there are better algorithms, however, we know the
      // bits are always right aligned, with no holes (i.e. always 00000111,
      // never 000100011), so this is just fine...
      //
      short value = 0;
      while ((mask & 0x1) != 0) {
        value++;
        mask >>= 1;
      }
      return value;
    }

    /// <devdoc>
    ///    <para> Creates the first mask in a series.</para>
    /// </devdoc>
    public static int CreateMask() {
      return CreateMask(0);
    }

    /// <devdoc>
    ///     Creates the next mask in a series.
    /// </devdoc>
    public static int CreateMask(int previous) {
      if (previous == 0) {
        return 1;
      }

      if (previous == unchecked((int)0x8000)) {
        throw new InvalidOperationException("");
      }

      return previous << 1;
    }

    /// <devdoc>
    ///     Given a highValue, creates the mask
    /// </devdoc>
    private static short CreateMaskFromHighValue(short highValue) {
      short required = 16;
      while ((highValue & 0x8000) == 0) {
        required--;
        highValue <<= 1;
      }

      ushort value = 0;
      while (required > 0) {
        required--;
        value <<= 1;
        value |= 0x1;
      }

      return unchecked((short)value);
    }

    /// <devdoc>
    ///    <para>Creates the first section in a series, with the specified maximum value.</para>
    /// </devdoc>
    public static Section CreateSection(short maxValue) {
      return CreateSectionHelper(maxValue, 0, 0);
    }

    /// <devdoc>
    ///    <para>Creates the next section in a series, with the specified maximum value.</para>
    /// </devdoc>
    public static Section CreateSection(short maxValue, Section previous) {
      return CreateSectionHelper(maxValue, previous.Mask, previous.Offset);
    }

    private static Section CreateSectionHelper(short maxValue, short priorMask, short priorOffset) {
      if (maxValue < 1) {
        throw new ArgumentException("maxValue");
      }
#if DEBUG
      int maskCheck = CreateMaskFromHighValue(maxValue);
      int offsetCheck = priorOffset + CountBitsSet(priorMask);
      Debug.Assert(maskCheck <= short.MaxValue && offsetCheck < 16, "Overflow on BitVector16");
#endif
      short offset = (short)(priorOffset + CountBitsSet(priorMask));
      if (offset >= 16) {
        throw new InvalidOperationException("");
      }
      return new Section(CreateMaskFromHighValue(maxValue), offset);
    }

    public override bool Equals(object o) {
      if (!(o is BitVector16)) {
        return false;
      }

      return data == ((BitVector16)o).data;
    }

    public override int GetHashCode() {
      return base.GetHashCode();
    }

    /// <devdoc>
    /// </devdoc>
    public static string ToString(BitVector16 value) {
      StringBuilder sb = new StringBuilder(/*"BitVector16{".Length*/12 + /*16 bits*/32 + /*"}".Length"*/1);
      sb.Append("BitVector16{");
      int locdata = (int)value.data;
      for (int i = 0; i < 16; i++) {
        if ((locdata & 0x8000) != 0) {
          sb.Append("1");
        } else {
          sb.Append("0");
        }
        locdata <<= 1;
      }
      sb.Append("}");
      return sb.ToString();
    }

    /// <devdoc>
    /// </devdoc>
    public override string ToString() {
      return BitVector16.ToString(this);
    }

    /// <devdoc>
    ///    <para> 
    ///       Represents an section of the vector that can contain a integer number.</para>
    /// </devdoc>
    public struct Section {
      private readonly short mask;
      private readonly short offset;

      internal Section(short mask, short offset) {
        this.mask = mask;
        this.offset = offset;
      }

      public short Mask {
        get {
          return mask;
        }
      }

      public short Offset {
        get {
          return offset;
        }
      }

      public override bool Equals(object o) {
        if (o is Section)
          return Equals((Section)o);
        else
          return false;
      }

      public bool Equals(Section obj) {
        return obj.mask == mask && obj.offset == offset;
      }

      public static bool operator ==(Section a, Section b) {
        return a.Equals(b);
      }

      public static bool operator !=(Section a, Section b) {
        return !(a == b);
      }

      public override int GetHashCode() {
        return base.GetHashCode();
      }

      /// <devdoc>
      /// </devdoc>
      public static string ToString(Section value) {
        return "Section{0x" + Convert.ToString(value.Mask, 16) + ", 0x" + Convert.ToString(value.Offset, 16) + "}";
      }

      /// <devdoc>
      /// </devdoc>
      public override string ToString() {
        return Section.ToString(this);
      }

    }
  }

  class UBV16 {
    // *** STATUS access values ***
    public static readonly int m1 = BitVector16.CreateMask(); //0
    public static readonly int m2 = BitVector16.CreateMask(m1); //1
    public static readonly int m3 = BitVector16.CreateMask(m2); //2
    public static readonly int m4 = BitVector16.CreateMask(m3); //3
    public static readonly int m5 = BitVector16.CreateMask(m4); //3
    public static readonly int m6 = BitVector16.CreateMask(m5); //3
    public static readonly int m7 = BitVector16.CreateMask(m6); //3
    public static readonly int m8 = BitVector16.CreateMask(m7); //3
    public static readonly int m9 = BitVector16.CreateMask(m8); //3
    public static readonly int m10 = BitVector16.CreateMask(m9); //3
    public static readonly int m11 = BitVector16.CreateMask(m10); //3
    public static readonly int m12 = BitVector16.CreateMask(m11); //3
    public static readonly int m13 = BitVector16.CreateMask(m12); //3
    public static readonly int m14 = BitVector16.CreateMask(m13); //3
    public static readonly int m15 = BitVector16.CreateMask(m14); //3
    public static readonly int m16 = BitVector16.CreateMask(m15); //3

    public BitVector16 state = new BitVector16(0);

    public string GetRepr() {
      List<string> str = new List<string>();
      str.Add(state[m1] ? "1" : "0");
      str.Add(state[m2] ? "1" : "0");
      str.Add(state[m3] ? "1" : "0");
      str.Add(state[m4] ? "1" : "0");
      str.Add(state[m5] ? "1" : "0");
      str.Add(state[m6] ? "1" : "0");
      str.Add(state[m7] ? "1" : "0");
      str.Add(state[m8] ? "1" : "0");
      str.Add(state[m9] ? "1" : "0");
      str.Add(state[m10] ? "1" : "0");
      str.Add(state[m11] ? "1" : "0");
      str.Add(state[m12] ? "1" : "0");
      str.Add(state[m13] ? "1" : "0");
      str.Add(state[m14] ? "1" : "0");
      str.Add(state[m15] ? "1" : "0");
      str.Add(state[m16] ? "1" : "0");
      return string.Join(", ", str);
    }
  }


  class Bitvec16Test {
    public static void Run() {
      UBV16 ssd = new UBV16();
      ssd.state[UBV16.m1] = true;
      Console.WriteLine(ssd.GetRepr());
      ssd.state[UBV16.m9] = true;
      Console.WriteLine(ssd.GetRepr());
      ssd.state[UBV16.m16] = true;
      Console.WriteLine(ssd.GetRepr());
    }
  }
}
