using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Airtouch {
  public enum CleaningDirection {
    Left = 0,
    Right = 1
  };

  public static class GenUtils {
    // convert a shorts array to bytes array - not type casting but spreading the array to bytes
    public static byte[] FlattenShorts2Bytes(short[] shorts) {
      int sz = shorts.Length;
      byte[] results = new byte[sz * 2];
      for (int idx = 0; idx < sz; idx++)
        Array.Copy(BitConverter.GetBytes(shorts[idx]), 0, results, idx * 2, 2);
      return results;
    }

    public static short[] ExpandBytesToShorts(byte[] bytes) {
      int sz = bytes.Length;
      short[] results = new short[(int)((sz + 1) / 2)];
      Buffer.BlockCopy(bytes, 0, results, 0, sz);
      return results;
    }
  }

  public class PLCRow {
    public int rowNum { set; get; } = 0;
    public int trolleyPos { set; get; }
    public int trolleyDelta { set; get; }
    public int approachPos { set; get; }
    public int columnHeight { set; get; }
    public CleaningDirection side { set; get; }

    public bool IsValid() => rowNum != 0 && columnHeight != 0;

    public static PLCRow FromBuff(short[] data) {
      byte[] bytes = GenUtils.FlattenShorts2Bytes(data);
      PLCRow result = new PLCRow();
      result.rowNum = (int)BitConverter.ToInt16(bytes, 0);
      result.trolleyPos = BitConverter.ToInt32(bytes, 2);
      result.trolleyDelta = (int)BitConverter.ToInt16(bytes, 6);
      result.approachPos = (int)BitConverter.ToInt16(bytes, 8);
      result.columnHeight = (int)BitConverter.ToInt16(bytes, 10);
      short sideFlag = BitConverter.ToInt16(bytes, 12);
      result.side = sideFlag == 1 ? CleaningDirection.Right : CleaningDirection.Left;
      return result;
    }

    public List<short> ToBuff() {
      byte[] bytes = new byte[14];
      Array.Copy(BitConverter.GetBytes(rowNum), 0, bytes, 0, 2);
      Array.Copy(BitConverter.GetBytes(trolleyPos), 0, bytes, 2, 4);
      Array.Copy(BitConverter.GetBytes(trolleyDelta), 0, bytes, 6, 2);
      Array.Copy(BitConverter.GetBytes(approachPos), 0, bytes, 8, 2);
      Array.Copy(BitConverter.GetBytes(columnHeight), 0, bytes, 10, 2);
      Array.Copy(BitConverter.GetBytes(side == CleaningDirection.Right ? 1 : 0), 0, bytes, 12, 2);
      return GenUtils.ExpandBytesToShorts(bytes).ToList();
    }
  }

  public class PLCRows {
    public List<PLCRow> rows { set; get; } = new List<PLCRow>();

    public bool UpdatePLCRows(short[] rawRowsData) {
      List<PLCRow> plcRows = new List<PLCRow>();
      int idx = 0;
      while (true) {
        PLCRow plr = PLCRow.FromBuff(rawRowsData[idx..(idx + 7)]);
        if (!plr.IsValid())
          break;
        plcRows.Add(plr);
        idx += 7;
      }
      if (plcRows.Count > 0) // data is good
        rows = plcRows;
      return plcRows.Count > 0;
    }
  }

  public class MBData {
    public int address, data;
  }

  public class PlcConfig {
    public MBData[] plcAuxData = new MBData[100];
    public PLCRows plcRows { set; get; } = new PLCRows();
  }


  internal class JsonPLCConfig {
    public static void Run() {
      PlcConfig cfg = new PlcConfig();
      for (int idx =0; idx<100; idx++)
        cfg.plcAuxData[idx] = new 
            MBData() { address =900 + idx, data =idx* idx }
      ;
      cfg.plcRows = new PLCRows();
      for (int idx=0; idx<50; idx++)
        cfg.plcRows.rows.Add(new PLCRow(){
          rowNum = idx+1, columnHeight = idx*2, approachPos = idx+1, trolleyDelta = idx/2, 
          trolleyPos = idx*4000, side = idx % 2 == 0 ? CleaningDirection.Right : CleaningDirection.Left });
      // serialize
      //JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
      string str = JsonConvert.SerializeObject(cfg);
      Console.WriteLine("*******************************************************");
      Console.WriteLine(str);
      Console.WriteLine("*******************************************************");
      PlcConfig cfg2 = JsonConvert.DeserializeObject<PlcConfig>(str);
      Console.WriteLine(JsonConvert.SerializeObject(cfg2));
      Console.WriteLine("*******************************************************");
      Console.WriteLine(JsonConvert.SerializeObject(cfg2));
      Console.WriteLine("*******************************************************");
    }
  }
}
