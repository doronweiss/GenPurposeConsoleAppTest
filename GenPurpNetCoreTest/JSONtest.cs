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

    class JSONtest {
        public static void Run() {
            Test1AppXMLConfig cfg = new Test1AppXMLConfig();
            cfg.cells = new Test1CellDefinition[] { new Test1CellDefinition() { HeightMM = 10, XPosMM = 20, YPosMM = 30, cellId = 1, shelfNumber = 0 } };
            string json = JsonConvert.SerializeObject(cfg, Formatting.Indented);
            Console.WriteLine(json);
        }

    }
}
