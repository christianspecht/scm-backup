using NLog;
using System;
using System.Collections.Generic;

namespace ScmBackup.Loggers
{
    /// <summary>
    /// Logs to NLog
    /// </summary>
    internal class NLogLogger : ILogger
    {
        private Logger logger = LogManager.GetLogger("ScmBackup");

        public void Log(ErrorLevel level, string message, params object[] arg)
        {
            this.Log(level, null, message, arg);
        }

        public void Log(ErrorLevel level, Exception ex, string message, params object[] arg)
        {
            LogLevel NLogLevel = LogLevel.Trace;

            switch (level)
            {
                case ErrorLevel.Debug:
                    NLogLevel = LogLevel.Debug;
                    break;
                case ErrorLevel.Error:
                    NLogLevel = LogLevel.Error;
                    break;
                case ErrorLevel.Info:
                    NLogLevel = LogLevel.Info;
                    break;
                case ErrorLevel.Warn:
                    NLogLevel = LogLevel.Warn;
                    break;
            }

            this.logger.Log(NLogLevel, ex, message, arg);
        }

        public List<string> FilesToBackup
        {
            get { return new List<string> { "NLog.config" }; }
        }

        public void ExecuteOnExit(bool successful)
        {
        }
    }
}
