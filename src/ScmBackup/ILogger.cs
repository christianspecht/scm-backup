using System;

namespace ScmBackup
{
    /// <summary>
    /// Interface for logging
    /// </summary>
    internal interface ILogger
    {
        void Log(ErrorLevel level, string message, params object[] arg);
        void Log(ErrorLevel level, Exception ex, string message, params object[] arg);
    }
}
