using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Gorillas {
  internal static class CMACCalculator {
    // *** public implementation ***
    static public byte[] GetMIC(byte[] key, byte[] data) {
      return AESCMAC(key, data)[0..4];
    }

    // *** private implementation part ***
    static private byte[] AESEncrypt(byte[] key, byte[] iv, byte[] data) {
      using (MemoryStream ms = new MemoryStream()) {
        AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.None;

        using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write)) {
          cs.Write(data, 0, data.Length);
          cs.FlushFinalBlock();

          return ms.ToArray();
        }
      }
    }

    static private byte[] Rol(byte[] b) {
      byte[] r = new byte[b.Length];
      byte carry = 0;

      for (int i = b.Length - 1; i >= 0; i--) {
        ushort u = (ushort)(b[i] << 1);
        r[i] = (byte)((u & 0xff) + carry);
        carry = (byte)((u & 0xff00) >> 8);
      }

      return r;
    }

    static private byte[] AESCMAC(byte[] key, byte[] data) {
      // SubKey generation
      // step 1, AES-128 with key K is applied to an all-zero input block.
      byte[] L = AESEncrypt(key, new byte[16], new byte[16]);

      // step 2, K1 is derived through the following operation:
      byte[] FirstSubkey = Rol(L); //If the most significant bit of L is equal to 0, K1 is the left-shift of L by 1 bit.
      if ((L[0] & 0x80) == 0x80)
        FirstSubkey[15] ^= 0x87; // Otherwise, K1 is the exclusive-OR of const_Rb and the left-shift of L by 1 bit.

      // step 3, K2 is derived through the following operation:
      byte[] SecondSubkey = Rol(FirstSubkey); // If the most significant bit of K1 is equal to 0, K2 is the left-shift of K1 by 1 bit.
      if ((FirstSubkey[0] & 0x80) == 0x80)
        SecondSubkey[15] ^= 0x87; // Otherwise, K2 is the exclusive-OR of const_Rb and the left-shift of K1 by 1 bit.

      // MAC computing
      if (((data.Length != 0) && (data.Length % 16 == 0)) == true) {
        // If the size of the input message block is equal to a positive multiple of the block size (namely, 128 bits),
        // the last block shall be exclusive-OR'ed with K1 before processing
        for (int j = 0; j < FirstSubkey.Length; j++)
          data[data.Length - 16 + j] ^= FirstSubkey[j];
      } else {
        // Otherwise, the last block shall be padded with 10^i
        byte[] padding = new byte[16 - data.Length % 16];
        padding[0] = 0x80;

        data = data.Concat<byte>(padding.AsEnumerable()).ToArray();

        // and exclusive-OR'ed with K2
        for (int j = 0; j < SecondSubkey.Length; j++)
          data[data.Length - 16 + j] ^= SecondSubkey[j];
      }

      // The result of the previous process will be the input of the last encryption.
      byte[] encResult = AESEncrypt(key, new byte[16], data);

      byte[] HashValue = new byte[16];
      Array.Copy(encResult, encResult.Length - HashValue.Length, HashValue, 0, HashValue.Length);
      //Array.Copy(encResult, 0, HashValue, 0, HashValue.Length);

      return HashValue;
    }

  }

  internal class MICCalculator {
    public static string Bytes2HexString(byte[] data) {
      StringBuilder sb = new StringBuilder();
      data.ToList().ForEach(x => sb.Append(x.ToString("X2")));
      return sb.ToString();
    }

    public static void Run() {
      byte[] NwkSKey = { 0x15, 0xb1, 0xd0, 0xef, 0xa4, 0x63, 0xdf, 0xbe, 0x3d, 0x11, 0x18, 0x1e, 0x1e, 0xc7, 0xda, 0x85 };
      byte[] AppSKey = { 0xd7, 0x2c, 0x78, 0x75, 0x8c, 0xdc, 0xca, 0xbf, 0x55, 0xee, 0x4a, 0x77, 0x8d, 0x16, 0xef, 0x67 };
      byte [] appKey =  {0x3f, 0x6b, 0xd6, 0x44, 0x69, 0xc6, 0xea, 0xd4, 0x38, 0xcd, 0x2c, 0xd6, 0xdb, 0xca, 0x61, 0xac};
      byte[] key = appKey;
      Console.Write("Enter data: ");
      string message = Console.ReadLine();
      byte[] data = Convert.FromBase64String(message);
      Console.WriteLine($"Message: {Bytes2HexString(data)}");
      byte[] mic = CMACCalculator.GetMIC(key, data[0..19]);
      Console.WriteLine($"MIC: {Bytes2HexString(data[19..23])}  Calced: {Bytes2HexString(mic)}");
    }
  }
}
