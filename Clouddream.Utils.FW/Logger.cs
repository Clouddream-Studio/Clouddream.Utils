using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

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
            string methodName = new StackTrace().GetFrame(0).GetMethod().Name;
            string assemblyName = Assembly.GetCallingAssembly().GetName().Name;
            if (logType != LogType.OnlyLogFile)
            {
                ConsoleColor consoleColor = Console.ForegroundColor;
                switch (logLevel)
                {
                    case LogLevel.WARN:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case LogLevel.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
                Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss} {logLevel}] [{assemblyName} - {methodName}] {message}");
                Console.ForegroundColor = consoleColor;
            }
            if (logType != LogType.OnlyConsole)
            {
                File.AppendAllLines(string.IsNullOrWhiteSpace(logFilePath) ? $"{DateTime.Now:yyyy-MM-dd}" : logFilePath, new[] { $"[{DateTime.Now} {logLevel}] [{assemblyName} - {methodName}] {message}" });
            }
#if !DEBUG
        }
#endif
    }
}
