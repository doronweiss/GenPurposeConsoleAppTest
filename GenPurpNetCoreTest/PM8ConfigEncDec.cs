using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace GenPurpNetCoreTest {

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
    public string type;
    public List<OptionValueType> options;
    public string[] validation;
  }

  public class CategoryProps {
    public string displayKey;
    public List<SingleValueData> data;
  }

  public class CategoryType {
    public string categoryKey;
    public CategoryProps categoryProperties;
  }

  public class SettingsDefs {
    public List<CategoryType> categories;
  }


  class PM8ConfigEncDec {
    public static void Run() {
      SettingsDefs sd = new SettingsDefs() {categories = new List<CategoryType>()};
      // category general
      sd.categories.Add(new CategoryType() {
        categoryKey = "general",
        categoryProperties = new CategoryProps() {displayKey = "the general data"}
      });
      sd.categories[0].categoryProperties.data = new List<SingleValueData>();
      List<SingleValueData> svds = sd.categories[0].categoryProperties.data; // just a shorthand
      //1
      SingleValueData svd= new SingleValueData() {
        key = "ip", displayKey = "Ip", currentValue = "192.168.1.125", isDisable = false, type = "string", validation = new[] {"required"}
      };
      svds.Add(svd);
      //2
      sd.categories[1].categoryProperties.data[0] = new SingleValueData() {
        key = "isDhcp", displayKey = "Is DHCP", currentValue = "true", isDisable = false, type = "bool", validation = new[] {"required"}
      };
      svds.Add(svd);
      // PLC config
      sd.categories.Add(new CategoryType() {
        categoryKey = "PLCConfigParameters",
        categoryProperties = new CategoryProps() {displayKey = "PLC ponfig parameters"}
      });
      sd.categories[1].categoryProperties.data = new List<SingleValueData>();
      svds = sd.categories[1].categoryProperties.data; // just a shorthand
      //1
      svd = new SingleValueData() {
        key = "travelSpeedXMaxMM2S", displayKey = "Travel speed X max [mm/s]", 
        currentValue = "250", isDisable = false, type = "int", validation = new[] {"required"}
      };
      svds.Add(svd);
      //2
      svd = new SingleValueData() {
        key = "travelSpeedYMaxMM2S", displayKey = "Travel speed Y max [mm/s]",
        currentValue = "250", isDisable = false, type = "int", validation = new[] {"required"}
      };
      svds.Add(svd);

      string json = JsonConvert.SerializeObject(sd, Formatting.Indented);
      Console.WriteLine(json);
    }
  }
}
