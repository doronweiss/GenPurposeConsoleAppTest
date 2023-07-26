// See https://aka.ms/new-console-template for more information
using Loggers;

AppLogger.Init();
AppLogger.LogDebug("hello doron weiss", "", LogDisplayTypeEnum.Notice);
for (int idx = 0; idx < 100; idx++) {
  AppLogger.LogDebug($"[{idx}]hello doron weiss", "", LogDisplayTypeEnum.Notice);
  System.Threading.Thread.Sleep(2000);
}

