//#define WRITETOCONSOLE
#define WRITETOBOTH
using System.Globalization;
using NLog;
using NLog.Layouts;
using NLog.Targets;

// https://blog.elmah.io/nlog-tutorial-the-essential-guide-for-logging-from-csharp/

namespace Loggers {
  public enum LogMessageLevel {
    Debug,
    Warning,
    Error,
    Fatal,
    Exception
  }

  public enum LogDisplayTypeEnum {
    Debug,
    Warning,
    Error,
    Fatal,
    Exception,
    ToRobot,
    FromRobot,
    FromUndefinedRobot,
    PLCCommand,
    Notice,
    Important
  }

  public class LogFileDesc {
    public DateTime timeStamp { get; set; }
    public string fileName { get; set; }
  }

  public enum LogOutputType {
    Diagnostics,
    Console,
    Both,
    None
  }

  public delegate void LogMethodDelegate(LogDisplayTypeEnum dispType, LogMessageLevel level, string title, string details, bool send2cloud);

  public static class AppLogger {
    public enum GUIDisplayEnum { LogOnly, Display };
    private static NLog.Logger log;
    private static bool log2Console = false;
    private static LogOutputType debugOutputType;

    public static void Init() {
      DateTime dt = DateTime.Now;
      AppLogger.debugOutputType = LogOutputType.Console;
      string logFName = $"logs/{dt.Year:D4}{dt.Month:D2}{dt.Day:D2} {dt.Hour:D2}{dt.Minute:D2}{dt.Second:D2}.log";
      logFName = Path.Combine(Directory.GetCurrentDirectory(), logFName);
      var config = new NLog.Config.LoggingConfiguration();
      config.Variables["basedir"] = Path.Combine(Directory.GetCurrentDirectory(), "logs");
      Target logfile = new FileTarget() {
        ArchiveEvery = FileArchivePeriod.Minute,
        ArchiveFileName = $"{Directory.GetCurrentDirectory()}/logs/" + "log.{#}.log",
        ArchiveNumbering = ArchiveNumberingMode.Date,
        ArchiveDateFormat = "yyyyMMdd HHmmss",
        MaxArchiveDays = 15,
        AutoFlush = true,
        //FileName = $"{Directory.GetCurrentDirectory()}/logs/${{date:format=yyyy-MM-dd_HHmmss}}.log",
        FileName = $"{Directory.GetCurrentDirectory()}/logs/current ${{shortdate}}.log",
        Name = "file"
      };
      config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
      NLog.LogManager.Configuration = config;
      log = NLog.LogManager.GetLogger("main");
    }

    public static void SetOutputType(LogOutputType debugOutputType)=>
      AppLogger.debugOutputType = debugOutputType;

    #region Color helper methods
    private static ConsoleColor GetFGColor(LogDisplayTypeEnum dispType) {
      return dispType switch {
        LogDisplayTypeEnum.Debug => ConsoleColor.White,
        LogDisplayTypeEnum.Warning => ConsoleColor.Magenta,
        LogDisplayTypeEnum.Error => ConsoleColor.Red,
        LogDisplayTypeEnum.Fatal => ConsoleColor.Red,
        LogDisplayTypeEnum.Exception => ConsoleColor.Red,
        LogDisplayTypeEnum.ToRobot => ConsoleColor.Blue,
        LogDisplayTypeEnum.FromRobot => ConsoleColor.Green,
        LogDisplayTypeEnum.FromUndefinedRobot => ConsoleColor.Yellow,
        LogDisplayTypeEnum.PLCCommand => ConsoleColor.Cyan,
        LogDisplayTypeEnum.Notice => ConsoleColor.Cyan,
        LogDisplayTypeEnum.Important => ConsoleColor.Yellow,
        _ => ConsoleColor.Black
      };
    }

    private static ConsoleColor GetBGColor(LogDisplayTypeEnum dispType) {
      return dispType switch {
        LogDisplayTypeEnum.Debug => ConsoleColor.Black,
        LogDisplayTypeEnum.Warning => ConsoleColor.Black,
        LogDisplayTypeEnum.Error => ConsoleColor.Gray,
        LogDisplayTypeEnum.Fatal => ConsoleColor.Gray,
        LogDisplayTypeEnum.Exception => ConsoleColor.Gray,
        LogDisplayTypeEnum.ToRobot => ConsoleColor.Black,
        LogDisplayTypeEnum.FromRobot => ConsoleColor.Black,
        LogDisplayTypeEnum.FromUndefinedRobot => ConsoleColor.Black,
        LogDisplayTypeEnum.PLCCommand => ConsoleColor.Black,
        LogDisplayTypeEnum.Notice => ConsoleColor.Black,
        LogDisplayTypeEnum.Important => ConsoleColor.Black,
        _ => ConsoleColor.Black
      };
    }

    public static void WriteConsoleWithColor(string message, ConsoleColor fg, ConsoleColor bg) {
      ConsoleColor bgClr = Console.BackgroundColor;
      ConsoleColor fgClr = Console.ForegroundColor;
      Console.BackgroundColor = bg;
      Console.ForegroundColor = fg;
      Console.WriteLine(message);
      Console.BackgroundColor = bgClr;
      Console.ForegroundColor = fgClr;
    }

    #endregion Color helper methods

    private static void Write(string message, LogDisplayTypeEnum? dispType = null) {
      void WriteToConsole(string message, LogDisplayTypeEnum? dispType = null) {
        if (dispType == null)
          Console.WriteLine(message);
        else {
          ConsoleColor fg = GetFGColor((LogDisplayTypeEnum)dispType);
          ConsoleColor bg = GetBGColor((LogDisplayTypeEnum)dispType);
          WriteConsoleWithColor(message, fg, bg);
        }
      }
      switch (debugOutputType) {
        case LogOutputType.Diagnostics:
          System.Diagnostics.Debug.WriteLine(message);
          break;
        case LogOutputType.Console:
          WriteToConsole(message, dispType);
          break;
        case LogOutputType.Both:
          System.Diagnostics.Debug.WriteLine(message);
          WriteToConsole(message, dispType);
          break;
        case LogOutputType.None:
          break;
      }
    }

    public static void LogDebug(string title, string logMsg, LogDisplayTypeEnum? dispType = null) {
      log?.Debug($"{title} | {logMsg}");
      Write($"Debug: {title} // {logMsg}", dispType);
    }

    public static void LogWarning(string title, string logMsg, LogDisplayTypeEnum? dispType = null) {
      log?.Warn($"{title} | {logMsg}");
      Write($"Warning: {title} // {logMsg}", dispType);
    }

    public static void LogError(string title, string logMsg, LogDisplayTypeEnum? dispType = null) {
      log?.Error($"{title} | {logMsg}");
      Write($"Error: {title} // {logMsg}", dispType);
    }

    public static void LogFatal(string title, string logMsg, LogDisplayTypeEnum? dispType = null) {
      log?.Fatal($"{title} | {logMsg}");
      Write($"Exception: {title} : {logMsg}", dispType);
    }

    public static void LogException(string title, string logMsg, Exception ex, LogDisplayTypeEnum? dispType = null) {
      log.Fatal($"{title} | {ex}");
      log.Fatal($"Exception drill down | {logMsg}");
      Write($"Exception: {title} ", LogDisplayTypeEnum.Exception);
    }

    // get a list of all log files
    public static List<LogFileDesc> GetLogs() {
      string[] fnames = Directory.GetFiles("logs", "*.log");
      List<LogFileDesc> lfDescs = new List<LogFileDesc>();
      foreach (string fname in fnames) {
        if (fname.Length < 20)
          continue;
        string tstr = Path.GetFileName(fname);
        tstr = Path.GetFileNameWithoutExtension(tstr).Substring(15);
        if (DateTime.TryParseExact(tstr.Replace("_D", ""), "yyyyMMdd HHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fileDt))
          lfDescs.Add(new LogFileDesc() { timeStamp = fileDt, fileName = fname });
      }
      return lfDescs.OrderByDescending(x => x.timeStamp).ToList();
    }


  }
}