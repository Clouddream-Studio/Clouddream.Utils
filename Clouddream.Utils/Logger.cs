using System;
using System.IO;
using System.Reflection;

namespace Clouddream.Utils
{
    public class Logger
    {
        public enum LogLevel
        {
            DEBUG = 0,
            INFO = 1,
            WARN = 2,
            ERROR = 3,
            FATAL = 4
        }

        public enum LogType
        {
            OnlyConsole = 0,
            OnlyLogFile = 1,
            ConsoleWithLogFile = 2
        }

        public static void Trace(string message, LogLevel logLevel = LogLevel.INFO, LogType logType = LogType.OnlyConsole, string logFilePath = "")
        {
#if !DEBUG
            if (logLevel != LogLevel.DEBUG)
            {
#endif
            switch (logType)
            {
                case LogType.OnlyConsole:
                    {
                        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss} {logLevel}] [{Assembly.GetCallingAssembly().GetName().Name} - {MethodBase.GetCurrentMethod().Name}] {message}");
                        break;
                    }
                case LogType.OnlyLogFile:
                    {
                        File.AppendAllLines(string.IsNullOrWhiteSpace(logFilePath) ? $"{DateTime.Now:yyyy-MM-dd}" : logFilePath, new[] { $"[{DateTime.Now} {logLevel}] [{Assembly.GetCallingAssembly().GetName().Name} - {MethodBase.GetCurrentMethod().Name}] {message}" });
                        break;
                    }
                case LogType.ConsoleWithLogFile:
                    {
                        Console.WriteLine($"[{DateTime.Now:yyyy - MM - dd HH:mm:ss} {logLevel}] {message}");
                        File.AppendAllLines(string.IsNullOrWhiteSpace(logFilePath) ? $"{DateTime.Now:yyyy-MM-dd}" : logFilePath, new[] { $"[{DateTime.Now} {logLevel}] [{Assembly.GetCallingAssembly().GetName().Name} - {MethodBase.GetCurrentMethod().Name}] {message}" });
                        break;
                    }
            }
#if !DEBUG
            }
#endif
        }
    }
}
