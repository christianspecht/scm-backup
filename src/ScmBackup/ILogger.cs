using System;

namespace ScmBackup
{
    /// <summary>
    /// Interface for logging
    /// </summary>
    internal interface ILogger
    {
        void Log(LogLevel level, string message, params object[] arg);
        void Log(LogLevel level, Exception ex, string message, params object[] arg);
    }
}
