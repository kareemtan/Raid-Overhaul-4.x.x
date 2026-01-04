using SPTarkov.Server.Core.Models.Logging;
using SPTarkov.Server.Core.Models.Spt.Logging;
using SPTarkov.Server.Core.Models.Utils;

namespace RaidOverhaulMain.Helpers;

public static class ROLogger
{
    private const string LogPrefix = "[Raid Overhaul] ";

    public static void Log<T>(ISptLogger<T> logger, string message, LogTextColor textColor = LogTextColor.White)
    {
        logger.LogWithColor(LogPrefix + message, textColor);
    }

    public static void LogDebug<T>(ISptLogger<T> logger, string message)
    {
        if (logger.IsLogEnabled(LogLevel.Debug))
        {
            logger.Debug(LogPrefix + message);
        }
    }

    public static void LogInfo<T>(ISptLogger<T> logger, string message)
    {
        if (logger.IsLogEnabled(LogLevel.Info))
        {
            logger.Info(LogPrefix + message);
        }
    }

    public static void LogWarning<T>(ISptLogger<T> logger, string message)
    {
        if (logger.IsLogEnabled(LogLevel.Warn))
        {
            logger.Warning(LogPrefix + message);
        }
    }

    public static void LogError<T>(ISptLogger<T> logger, string message)
    {
        if (logger.IsLogEnabled(LogLevel.Error))
        {
            logger.Error(LogPrefix + message);
        }
    }

    public static void LogToServer<T>(ISptLogger<T> logger, string message, LogTextColor textColor = LogTextColor.White)
    {
        logger.LogWithColor(LogPrefix + message, textColor);
    }
}
