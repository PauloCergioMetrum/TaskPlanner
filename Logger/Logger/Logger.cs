using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System.Text;

namespace Memt.Logger
{
  public class Logger
  {
    private static string logNameSpace = "Log";
    private static readonly string logFileNamePrefix = $@"{Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\Logs\";
    private static ILog? log;
    private static bool isDebugMode = false;
    private static readonly PatternLayout patternlayout = new() { ConversionPattern = "%d{dd-MM-yyyy HH:mm:ss.fff} %c %-5p-> %m%n" };
    private static readonly List<IAppender> appenders = new();

    private Logger() { }

    public static void CreateLogger()
    {
      logNameSpace = "Log";
    }
    public static void CreateLogger(string LogNameSpace)
    {
      logNameSpace = LogNameSpace;
    }
    public static void AddNewAppender(EAppenderType appenderType)
    {
      switch (appenderType)
      {
        case EAppenderType.Console:
          ConsoleAppender CAP = new()
          {
            Name = logNameSpace + "_CAP",
            Layout = patternlayout
          };
          appenders.Add(CAP);
          break;
        case EAppenderType.ColoredConsole:
          ColoredConsoleAppender CCA = new()
          {
            Name = logNameSpace + "_CCA",
            Layout = patternlayout
          };
          CCA.AddMapping(new() { Level = Level.Info, ForeColor = ColoredConsoleAppender.Colors.Green });
          CCA.AddMapping(new() { Level = Level.Debug, ForeColor = ColoredConsoleAppender.Colors.White });
          CCA.AddMapping(new() { Level = Level.Warn, ForeColor = ColoredConsoleAppender.Colors.Yellow });
          CCA.AddMapping(new() { Level = Level.Error, BackColor = ColoredConsoleAppender.Colors.Red, ForeColor = ColoredConsoleAppender.Colors.White });
          appenders.Add(CCA);
          break;
        case EAppenderType.File:
          RollingFileAppender RFA = new()
          {
            Name = logNameSpace + "_RFA",                               // Set name of appender
            File = logFileNamePrefix + logNameSpace,                    // Set file name prefix
            LockingModel = new FileAppender.MinimalLock(),              // Minimum lock time required, makes file available for reading
            AppendToFile = true,                                        // Do not overwrite existing logs, append to them.
            DatePattern = ".yyyy.MM.dd'.log'",                          // Add file extension here, to preserve the file extension
            Encoding = Encoding.UTF8,                                   // Set format of file to UTF8 for international characters.
            CountDirection = 1,                                         // Increment file name in bigger number is newest, instead of default backward.
            MaximumFileSize = "100MB",                                  // Maximum size of file that I could open with common notepad applications
            RollingStyle = RollingFileAppender.RollingMode.Composite,   // Increment file names by both size and date.
            StaticLogFileName = false,
            MaxSizeRollBackups = -1,                                    // Keep all log files, do not automatically delete any
            PreserveLogFileNameExtension = true,                        // This plus extension added to DatePattern, causes to rolling size also work correctly
            Layout = patternlayout
          };
          RFA.ActivateOptions();
          appenders.Add(RFA);
          break;
      }
    }

    public static void Activate()
    {
      log = LogManager.GetLogger(logNameSpace);
      Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
      patternlayout.ActivateOptions();
      foreach (var appender in appenders)
      {
        hierarchy.Root.AddAppender(appender);
      }
      BasicConfigurator.Configure(hierarchy);
      foreach (IAppender appender in hierarchy.Root.Appenders)
      {
        if (appender.Name == null)
        {
          appender.Close();
        }
      }
    }
    public static void EnableDebugMode() => isDebugMode = true;
    public static void DisableDebugMode() => isDebugMode = false;

    public static void SetLogLevel(ELoggerType value)
    {
      Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
      switch (value)
      {
        case ELoggerType.Info:
          hierarchy.Threshold = Level.Info;
          break;
        case ELoggerType.Debug:
          hierarchy.Threshold = Level.Debug;
          break;
        case ELoggerType.Warn:
          hierarchy.Threshold = Level.Warn;
          break;
        case ELoggerType.Error:
          hierarchy.Threshold = Level.Error;
          break;
        case ELoggerType.Fatal:
          hierarchy.Threshold = Level.Fatal;
          break;
      }
    }
    public static void Log(string Description, ELoggerType logtype = ELoggerType.Info)
    {
      if (isDebugMode)
      {
        switch (logtype)
        {
          case ELoggerType.Info:
            log?.Info(Description);
            break;
          case ELoggerType.Debug:
            log?.Debug(Description);
            break;
          case ELoggerType.Warn:
            log?.Warn(Description);
            break;
          case ELoggerType.Error:
            log?.Error(Description);
            break;
          case ELoggerType.Fatal:
            log?.Fatal(Description);
            break;
        }
      }
    }
    public static void Log(string Message, Exception ex)
    {
      if (isDebugMode)
      {
        log?.Fatal(Message, ex);
      }
    }
  }
}

