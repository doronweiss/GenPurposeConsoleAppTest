using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {

  public struct BitVector64 {
    private ulong data;

    /// <devdoc>
    /// <para>Initializes a new instance of the BitVector64 structure with the specified internal data.</para>
    /// </devdoc>
    public BitVector64(int data) {
      this.data = (ulong)data;
    }

    /// <devdoc>
    /// <para>Initializes a new instance of the BitVector64 structure with the information in the specified 
    ///    value.</para>
    /// </devdoc>
    public BitVector64(BitVector64 value) {
      this.data = value.data;
    }

    /// <devdoc>
    ///    <para>Gets or sets a value indicating whether all the specified bits are set.</para>
    /// </devdoc>
    public bool this[ulong bit] {
      get {
        return (data & bit) == (ulong)bit;
      }
      set {
        if (value) {
          data |= (ulong)bit;
        } else {
          data &= ~(ulong)bit;
        }
      }
    }

    /// <devdoc>
    ///    <para>Gets or sets the value for the specified section.</para>
    /// </devdoc>
    public ulong this[Section section] {
      get {
        return (ulong)((data & (ulong)(section.Mask << section.Offset)) >> section.Offset);
      }
      set {
#if DEBUG
        if ((value & section.Mask) != value) {
          Debug.Fail("Value out of bounds on BitVector64 Section Set!");
        }
#endif
        value <<= section.Offset;
        int offsetMask = (0xFFFF & (int)section.Mask) << section.Offset;
        data = (data & ~(ulong)offsetMask) | ((ulong)value & (ulong)offsetMask);
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

    private static short CountBitsSet(ulong mask) {

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
    public static ulong CreateMask() {
      return CreateMask(0);
    }

    /// <devdoc>
    ///     Creates the next mask in a series.
    /// </devdoc>
    public static ulong CreateMask(ulong previous) {
      if (previous == 0) {
        return 1;
      }

      if (previous == unchecked((ulong)0x80000000)) {
        throw new InvalidOperationException("previous == unchecked((ulong)0x80000000)");
      }

      return previous << 1;
    }

    /// <devdoc>
    ///     Given a highValue, creates the mask
    /// </devdoc>
    private static ulong CreateMaskFromHighValue(ulong highValue) {
      ulong required = 16;
      while ((highValue & 0x8000) == 0) {
        required--;
        highValue <<= 1;
      }

      ulong value = 0;
      while (required > 0) {
        required--;
        value <<= 1;
        value |= 0x1;
      }

      return unchecked((ulong)value);
    }

    /// <devdoc>
    ///    <para>Creates the first section in a series, with the specified maximum value.</para>
    /// </devdoc>
    public static Section CreateSection(ulong maxValue) {
      return CreateSectionHelper(maxValue, 0, 0);
    }

    /// <devdoc>
    ///    <para>Creates the next section in a series, with the specified maximum value.</para>
    /// </devdoc>
    public static Section CreateSection(ulong maxValue, Section previous) {
      return CreateSectionHelper(maxValue, previous.Mask, previous.Offset);
    }

    private static Section CreateSectionHelper(ulong maxValue, ulong priorMask, short priorOffset) {
      if (maxValue < 1) {
        throw new ArgumentException("maxValue < 1");
      }
#if DEBUG
      ulong maskCheck = CreateMaskFromHighValue(maxValue);
      short offsetCheck = (short)(priorOffset + CountBitsSet(priorMask));
      Debug.Assert(maskCheck <= ulong.MaxValue && offsetCheck < 64, "Overflow on BitVector64");
#endif
      short offset = (short)(priorOffset + CountBitsSet(priorMask));
      if (offset >= 64) {
        throw new InvalidOperationException("offset >= 64");
      }
      return new Section(CreateMaskFromHighValue(maxValue), offset);
    }

    public override bool Equals(object o) {
      if (!(o is BitVector64)) {
        return false;
      }

      return data == ((BitVector64)o).data;
    }

    public override int GetHashCode() {
      return base.GetHashCode();
    }

    /// <devdoc>
    /// </devdoc>
    public static string ToString(BitVector64 value) {
      StringBuilder sb = new StringBuilder(/*"BitVector64{".Length*/12 + /*32 bits*/32 + /*"}".Length"*/1);
      sb.Append("BitVector64{");
      ulong locdata = (ulong)value.data;
      for (int i = 0; i < 64; i++) {
        if ((locdata & 0x80000000) != 0) {
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
      return BitVector64.ToString(this);
    }

    /// <devdoc>
    ///    <para> 
    ///       Represents an section of the vector that can contain a integer number.</para>
    /// </devdoc>
    public struct Section {
      private readonly ulong mask;
      private readonly short offset;

      internal Section(ulong mask, short offset) {
        this.mask = mask;
        this.offset = offset;
      }

      public ulong Mask {
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
        return "Section{0x";// + Convert.ToString(value.Mask, 16) + ", 0x" + Convert.ToString(value.Offset, 16) + "}";
      }

      /// <devdoc>
      /// </devdoc>
      public override string ToString() {
        return Section.ToString(this);
      }

    }
  }

  class BitVector64Test {
    public static void Run() {
      BitVector64 bv64 = new BitVector64();
      ulong m1 = BitVector64.CreateMask();
      ulong m2 = BitVector64.CreateMask(m1);
      ulong m3 = BitVector64.CreateMask(m2);
      ulong m4 = BitVector64.CreateMask(m3);
      ulong m5 = BitVector64.CreateMask(m4);

      bv64[m1] = true;
      bv64[m2] = false;
      bv64[m3] = true;
      bv64[m4] = false;
      bv64[m5] = true;

      Console.WriteLine($"has: {bv64[m1]} {bv64[m2]} {bv64[m3]} {bv64[m4]} {bv64[m5]}");
    }
  }
}
