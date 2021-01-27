using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SettingJSONTest;

namespace SettingJSONTest {
  #region ENUMS
  public enum MissingSetting {
    StationNum = 1,
    StationModelID = 2,
    ServerIP = 3,
    ServerPort = 4,
    CommPortConflict = 5
  }

  public enum LimitsMode {
    Undefined = 0,
    SimpleLimits = 1,
    ExtendedLimits = 2,
    FullLimits = 3,
    CreditCards = 4
  }

  /// Return station's mechanism for removing item from cell
  public enum ItemClearMethod {
    Undefined = 0,
    Shelf = 1,
    Blower = 2
  }

  public enum RFIDdviceType {
    Unknown = 0,
    DataMars_HF = 1,
    DataMars_UHF1 = 2,
    DataMars_UHF2 = 3,
    DataMars_LF = 4,
    DataMars_ASST = 5 //Afet Stop Send Tag
  }
  #endregion

  #region base config classes
  [Serializable]
  public class CellDefinition {
    public int cellNumber;
    public int shelfNumber = 0; // what piston to push, 0=None
    public int XPosMM; // cell center X
    public int YPosMM; // cell top Y
    public int HeightMM; // cell top Y

    public static int GetSHSz() => (new CellDefinition()).ToShortsArray().Length;

    public short[] ToShortsArray() =>
      new short[] { (short)cellNumber, (short)XPosMM, (short)YPosMM, (short)HeightMM, (short)shelfNumber };
  }

  [Serializable]
  public class CardFormatsData {
    public int cardGroupStartIndex = 0;
    public string cardGroupChars = "9898";
    public int cardNumStartIndex = 0;
    public int cardNumCharsLength = 4;
  }
  #endregion base config classes

  #region Configuration parts classes
  [Serializable]
  public class PLCConfigParameters {
    public int travelSpeedXMaxMM2S;
    public int travelSpeedYMaxMM2S;
    public int travelSpeedXEemptyP;
    public int travelSpeedYEemptyP;
    public int BITTravelSpeedP;
    public int downInCellSpeedP;
    public int manualSpeedXP;
    public int manualSpeedyP;
    public int XHomePositionMM;
    public int YHomePositionMM;
    public int obtacleLeftMM;
    public int ostacleRightMM;
    public int obstacleTopMM;
    public int obstacleBottomMM;
    [XmlIgnore]
    public int numberOfCells;
    public int gripperExtraHeightOnFetchMM;
    public int distanceFromStraightGripperToDetect;
    public int stopDistanceFromGripperDetectMM;
    public int exitItemTimeMS;
    public int pistonTimeoutMS;
    public int homeCurrentLimitP;
    public int machineType;
    public int segmentTimeoutMS;
    public int processTimeoutMS;
    public int numberOfPistons;
    public List<CellDefinition> cells = new List<CellDefinition>();
  }

  [Serializable]
  public class PLCParameters {
    // connection
    public string plcIp = "192.168.0.250"; // plc addres and id
    public int plcPort = 502;
    public int plcDevId = 1;
    public int startAddress = 1;
    // timeouts
    public int MaxWaitForWriteMS = 200; // intervals and timeout values
    public int MainLoopDelayMS = 100;
    // get item operation
    public int deltaYExtraSpaceMM = 20;
  }

  [Serializable]
  public class PMDefinitions {
    public string ServerApiURL = "https://sc-api-dev2.polytex-technologies.com/api/StationCommunications/";
    public int pollIntervalMS = 1000;
    public int timeOutMS = 500;
  }

  [Serializable]
  public class CardReaderDefinitions {
    public string cardReaderCommPortName = "COM5";
    public int cardReaderCommBaud = 9600; // AKA CRCommPortBaudRate
    public char cardReaderEndOfIDChar = '\r';
    public bool isCardReaderHexFormat = false;
    public bool displayReturnItemWithOutCard = true;
    public bool encryptCardNum = false;
    public string cardReaderEncryptionFormat = "NONE";
    public string technicianCard = "1799";
    public bool passUIDAlsoIfFormatNotMatch = true;
    public CardFormatsData[] cardFormats;

    public CardReaderDefinitions() {
      cardFormats = new CardFormatsData[]
        {new CardFormatsData(), new CardFormatsData(), new CardFormatsData(), new CardFormatsData(), new CardFormatsData()};
    }
  }

  [Serializable]
  public class RFTagReaderDefinitions {
    public bool enableRFIDDevice = false;
    public string rfidControllerCommPortName = "COM8";
    public int rfidControllerCommPortBaudRate = 115200;
    public RFIDdviceType rfidControllerDeviceTypeID = RFIDdviceType.DataMars_HF;
    public int timeoutRFIDwaitASST = 300; // [ms]
    public char cardReaderEndOfIDChar = '\r';
    public char[] ignoreChars = new char[] { '\n' };
  }

  [Serializable]
  public class DispensingProcDefs {
    public LimitsMode limitsMode = LimitsMode.ExtendedLimits;
    public bool useRFId = true;
    public int PLC2RFTagMaxWaitMS = 500;
    public int numOfRetiesOnItemNotDetected = 3; // number of times we retry to pick an item from a cell if it is not detected on exit
    public int maxNumOfBlockedExitAlerts = 3;
  }

  [Serializable]
  public class UIDefinitions { }
  #endregion Configuration parts classes

  #region Non persisting data classes
  public class NonPersistData { }
  #endregion Non persisting data classes

  [Serializable]
  public class DeveloperOptions {
    public bool hasPLC = true;
    public bool hasPM8 = true;
    public bool hasPMonWSS = true;
    public bool hasRFTagRdr = true;
    public bool hasCardRdr = true;
    //
    public bool autoStart = false;
    public bool doBit = false;
  }

  [Serializable]
  [XmlRoot("AppXMLConfig")]
  public class AppXMLConfig {
    // station identification
    public int stationTypeID = 1;
    public int stationModelID = 100;
    public int stationNum = 1000224;
    // passwords
    public long operatorPW = 12121;
    public long technicianPW = 12321;
    // station network data
    public bool isDHCP = false;
    public string ipAddress = "192.168.1.106";
    public string subnetMask = "255.255.255.0";
    public string defaultGateway = "192.168.1.1";
    // main loop parameters
    public int coreLoopIntervalMS = 250;
    public int coreLoopUpdatesPushMS = 500;
    // poly manager communication definitions
    public PMDefinitions pmDefs = new PMDefinitions();
    // cells configuration
    public PLCConfigParameters plcCfgParams = new PLCConfigParameters();
    // plc parameters
    public PLCParameters PlcParams = new PLCParameters();
    // card reader
    public CardReaderDefinitions cardReaderDefs = new CardReaderDefinitions();
    // RF Tags reader
    public RFTagReaderDefinitions rfTagReaderDefs = new RFTagReaderDefinitions();
    // dispensing process
    public DispensingProcDefs dispensingDefs = new DispensingProcDefs();
    // UI definitions
    public UIDefinitions uiDefs = new UIDefinitions();
    // developer options - will be removed before deployment
    public DeveloperOptions devOptions = new DeveloperOptions();

    #region non-persistent members
    [XmlIgnore]
    public NonPersistData rtData = new NonPersistData();
    #endregion non-persistent members
  }

  #region SETTINGS JSON
  public enum JSONDataType {
    text, Number, boolean, checkbox, select, structureRowG5, cellDefsG6
  };

  public class OptionValueType {
    public string key;
    public string value;
  }

  public class SingleValueData {
    public string key;
    public string displayKey;
    public string currentValue;
    public string updateValue;
    public bool isDisable;
    [JsonIgnore]
    public JSONDataType dataType;
    public string type {
      get => dataType.ToString();
      set => dataType = Enum.Parse<JSONDataType>(value);
    }
    public List<OptionValueType> options = new List<OptionValueType>();
    public List<string> validation = new List<string>();
  }

  public class CategoryType {
    public string categoryKey;
    public string displayKey;
    public List<SingleValueData> data = new List<SingleValueData>();
  }

  public class CellDefs {
    public int cellNumber;
    public int shelfNumber = 0; // what piston to push, 0=None
    public int XPosMM; // cell center X
    public int YPosMM; // cell top Y
    public int HeightMM; // cell top Y
  }

  public class SettingsDefs {
    public List<CategoryType> categories = new List<CategoryType>();
    public List<CellDefs> cells = new List<CellDefs>();
  }
}
#endregion SETTINGS JSON

class Program {
  static SingleValueData CreateSVD(
    string key, string displayKey, string currentValue,
    bool isDisable, JSONDataType dataType,
    OptionValueType [] options, string [] validation) {
    SingleValueData svd = new SingleValueData() {
      key = key, displayKey = displayKey, currentValue = currentValue, updateValue = "", isDisable = isDisable, 
      dataType = dataType,  options  = options.ToList(), validation = validation.ToList()
    };
    return svd;
  }


  static SettingsDefs PackSettings() {
    SettingsDefs sd = new SettingsDefs() {categories = new List<CategoryType>()};
    // category general
    CategoryType ct = new CategoryType() {categoryKey = "general", displayKey = "the general data"};
    ct.data.Add(CreateSVD("ip", "Ip", "192.168.1.125", false, JSONDataType.text, new OptionValueType[]{}, new string[] {"required"}));
    ct.data.Add(CreateSVD("isDhcp", "Is DHCP", "true", false, JSONDataType.boolean, new OptionValueType[] { }, new string[] {"required"}));
    sd.categories.Add(ct);
    // PLC config
    ct = new CategoryType() {categoryKey = "PLCConfigParameters", displayKey = "PLC ponfig parameters"};
    ct.data.Add(CreateSVD("travelSpeedXMaxMM2S", "Travel speed X max [mm/s]", "250", false, JSONDataType.Number, new OptionValueType[] { }, new string[] {"required"}));
    ct.data.Add(CreateSVD("travelSpeedYMaxMM2S", "Travel speed Y max [mm/s]", "250", false, JSONDataType.Number, new OptionValueType[] { }, new string[] {"required"}));
    sd.categories.Add(ct);
    // cells
    sd.cells.Add(new CellDefs(){cellNumber = 211, shelfNumber = 0, XPosMM = 200, YPosMM = 10, HeightMM = 150});
    sd.cells.Add(new CellDefs(){cellNumber = 212, shelfNumber = 0, XPosMM = 300, YPosMM = 10, HeightMM = 150});
    sd.cells.Add(new CellDefs(){cellNumber = 213, shelfNumber = 0, XPosMM = 400, YPosMM = 10, HeightMM = 150});

    return sd;
  }

  static void Main(string[] args) {
    SettingsDefs sd = PackSettings();
    string json = JsonConvert.SerializeObject(sd, Formatting.Indented);
    SettingsDefs sdRes = JsonConvert.DeserializeObject<SettingsDefs>(json);
    Console.WriteLine(json);
  }
}

