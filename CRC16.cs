using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurposeConsoleAppTest {
  static class CRC16 {
    static readonly char[] AsciiChar =    {
   '0','1','2','3','4','5','6','7',
   '8','9','A','B','C','D','E','F'};
    const ushort generator = 0x8005;

    const bool reflectIn = true;
    const bool reflectOut = true;

    static public ushort calc_crc16(byte[] msg, int startIndex, ushort size) {
      ushort crc16=0; // CRC value is 16bit
      ushort i, j;
      byte bt;

      for (i = 0; i < size; i++) {
        //////// check if input bytes should be reflected /////////
        if (reflectIn)
          bt = Reflect8(msg[i + startIndex]); // RefIn == false
        else
          bt = msg[i + startIndex]; // RefIn == false

        /////// 16bit CRC calculation ///////////////////////////////////
        crc16 ^= (ushort)(bt << 8); // move byte into MSB of 16bit CRC
        for (j = 0; j < 8; j++)// calculate LSB of 16bit CRC
        {
          if ((crc16 & 0x8000) != 0) // test for MSB = bit15
          {
            crc16 <<= 1;
            crc16 ^= generator;
          } else {
            crc16 <<= 1;
          }
        }
      }
      if (reflectOut)
        return Reflect16(crc16);
      else
        return crc16;
    }

    // swap bits
    static byte Reflect8(byte val) {
      byte resVal = 0;
      for (int i = 0; i < 8; i++) {
        if ((val & (1 << i)) != 0) {
          resVal |= (byte)(1 << (7 - i));
        }
      }
      return resVal;
    }

    // swap ushort
    static ushort Reflect16(ushort val) {
      ushort resVal = 0;
      for (int i = 0; i < 16; i++) {
        if ((val & (1 << i)) != 0) {
          resVal |= (ushort)(1 << (15 - i));
        }
      }
      return resVal;
    }

    // put the CRC in the array
    static public void InsertCRC(byte[] buffer, int startIdx, ushort crc16) {
      string crcstr = crc16.ToString("X4");
      for (int idx = 0; idx < 4; idx++)
        buffer[startIdx + idx] = (byte)crcstr[idx];
    }
  }
}
