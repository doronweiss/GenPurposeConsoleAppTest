using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GenPurpNetCoreTest {
  public class Test1CellDefinition {
    public int cellId;
    public int shelfNumber = 0;     // what piston to push, 0=None
    public int XPosMM;              // cell center X
    public int YPosMM;              // cell top Y
    public int HeightMM;            // cell top Y
  }

  public class Test1PLCParameters {
    public int ControlWriteAddress = 100;
    public int StatusReadAddress = 110;
    public int MaxWaitForWriteMS = 100;
    public int MainLoopDelayMS = 100;
  }


  public class Test1AppXMLConfig {
    // station identification
    public int stationTypeID = 1;
    public int stationModelID = 100;
    // station network data
    public bool isDHCP = false;
    public string ipAddress = "10.0.0.150";
    public string subnetMask = "255.255.255.0";
    public string defaultGateway = "10.0.0.138";
    // cells configuration
    public Test1CellDefinition[] cells;
    // plc parameters
    public Test1PLCParameters Test1PlcParams = new Test1PLCParameters();
  }

  #region polymon json classes
  public class PolyRoot {
    public int optype;
    public int id;
    public string opData;

    public override string ToString() {
      return $"Optype={{optype}} , id={{id}}}}, data={opData}";
    }
  }

  public class PolyOp1 {
    public string name;
    public int anumber;

    public override string ToString() {
      return $"name={name} , anumber={anumber}";
    }
  }

  public class PolyOp2 {
    public int id;
    public int avalue;
    public string dummydata;

    public override string ToString() {
      return $"id={id} , avalue={avalue}, dummydata={dummydata}";
    }
  }


  #endregion polymon json classes

  class JSONtest {
    private static void Parseit(string json) {
      Console.WriteLine(json);
      PolyRoot pr = JsonConvert.DeserializeObject<PolyRoot>(json);
      switch (pr.optype) {
        case 1:
          PolyOp1 po1 = JsonConvert.DeserializeObject<PolyOp1>(pr.opData);
          Console.WriteLine(po1);
          break;
        case 2:
          PolyOp2 po2 = JsonConvert.DeserializeObject<PolyOp2>(pr.opData);
          Console.WriteLine(po2);
          break;
        default:
          break;
      }
    }

    public class CellsInv {
      public int CellNum { get; set; }
      public bool IsFull { get; set; }
    }

    public class Root {
      public List<CellsInv> CellsInv { get; set; }
    }

    public static void Run() {
      // test 1
      // Test1AppXMLConfig cfg = new Test1AppXMLConfig();
      // cfg.cells = new Test1CellDefinition[] { new Test1CellDefinition() { HeightMM = 10, XPosMM = 20, YPosMM = 30, cellId = 1, shelfNumber = 0 } };
      // string json = JsonConvert.SerializeObject(cfg, Formatting.Indented);
      // Console.WriteLine(json);
      // test 2
      // PolyOp1 po1 = new PolyOp1() { name = "moshe", anumber = 17 };
      // PolyOp2 po2 = new PolyOp2() { id = 53, avalue = 1973, dummydata = "kukuriku" };
      // PolyRoot pr = new PolyRoot() { optype = 1, id = 11, opData = JsonConvert.SerializeObject(po1) };
      // string json = JsonConvert.SerializeObject(pr);
      // Parseit(json);
      // pr = new PolyRoot() {optype = 2, id = 11, opData = JsonConvert.SerializeObject(po2)};
      // json = JsonConvert.SerializeObject(pr);
      // Parseit(json);

      // test 3
      Root r = new Root() {CellsInv = new List<CellsInv>()};
      r.CellsInv.Add(new CellsInv() {CellNum = 1, IsFull = true});
      r.CellsInv.Add(new CellsInv() {CellNum = 2, IsFull = false});
      r.CellsInv.Add(new CellsInv() {CellNum = 3, IsFull = true});
      r.CellsInv.Add(new CellsInv() {CellNum = 4, IsFull = false});
      string json = JsonConvert.SerializeObject(r);
      Console.WriteLine($"JSON: {json}");

    }

  }
}
