using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public enum LogLevel
{
    Minimal,
    Default,
    Verbose,
    Debug,
}

public static class Debug
{
    public static LogLevel level
    {
        get
        {
            if (Settings.settings == null)
                return LogLevel.Debug;

            return Settings.settings.loglevel;
        }
    }

    public static void Log(string text)
    {
        LogWithTime(LogLevel.Minimal, text, false);
    }

    public static void Log(LogLevel logLevel, string text, bool includeParentesis = true, bool isError = false)
    {
        if (isError)
            logLevel = LogLevel.Minimal;

        if (logLevel <= level)
        {
            string write = "";
            if (includeParentesis)
            {
                DateTime currTime = DateTime.Now;
                string paretnesis = '[' + currTime.ToString("T") + "]: ";
                Console.ForegroundColor = GetColor(logLevel);
                Console.Write(paretnesis); ;
            }

            Console.ForegroundColor = isError ? ConsoleColor.Red : ConsoleColor.Gray;
            Console.WriteLine(text);
        }
    }
    public static void LogWithTime(LogLevel logLevel, string text, bool isError = false)
    {
        if (logLevel <= level)
        {
            DateTime currTime = DateTime.Now;
            string paretnesis = '[' + currTime.ToString("T") + "]: ";
            Console.ForegroundColor = GetColor(logLevel);
            Console.Write(paretnesis);

            Log(logLevel, text, false, isError);
        }
    }
    public static void LogWithBacktrack(LogLevel logLevel, string text, bool isError = false)
    {
        if (logLevel <= level)
        {
            MethodBase mth = new StackTrace().GetFrame(1).GetMethod();

            string parentesis = '[' + mth.ReflectedType.Name + "." + mth.Name + "()]: ";
            Console.ForegroundColor = GetColor(logLevel);

            Console.Write(parentesis);
            Log(logLevel, text, false, isError);
        }
    }

    private static ConsoleColor GetColor(LogLevel logLevel)
    {
        ConsoleColor rtn;

        switch (logLevel)
        {
            case LogLevel.Minimal:
                rtn = ConsoleColor.Green;
                break;
            case LogLevel.Default:
                rtn = ConsoleColor.White;
                break;
            case LogLevel.Verbose:
                rtn = ConsoleColor.Gray;
                break;
            case LogLevel.Debug:
                rtn = ConsoleColor.Magenta;
                break;
            default:
                rtn = ConsoleColor.Gray;
                break;
        }

        return rtn;
    }

    #region Overload for objects log
    public static void Log(object text) { Log(text.ToString()); }
    public static void LogWithTime(LogLevel logLevel, object obj, bool isError = false)
    {
        LogWithTime(logLevel, obj.ToString(), isError);
    }
    public static void LogWithBacktrack(LogLevel logLevel, object obj, bool isError = false)
    {
        LogWithBacktrack(logLevel, obj.ToString(), isError);
    }
    #endregion
    #region TelepathyLogs
    public static void LogWarning(string str)
    {
        LogWithTime(LogLevel.Debug, str);
    }
    public static void LogError(string str)
    {
        LogWithTime(LogLevel.Minimal, str, true);
    }
    #endregion
}