using ScmBackup.Loggers;
using System;

namespace ScmBackup.Tests.Integration
{
    /// <summary>
    /// real logger for use in tests
    /// </summary>
    internal class TestLogger : ILogger
    {
        private readonly ILogger logger;

        public TestLogger(string logName)
        {
            // We are using the same logger like ScmBackup, but it's wrapped in this class.
            // So the real logger isn't hardcoded in lots of tests, should we ever change it.
            this.logger = new NLogLogger();

            this.Log(ErrorLevel.Info, "STARTING: " + logName);
        }

        public void Log(ErrorLevel level, string message, params object[] arg)
        {
            this.logger.Log(level, message, arg);
        }

        public void Log(ErrorLevel level, Exception ex, string message, params object[] arg)
        {
            this.logger.Log(level, ex, message, arg);
        }
    }
}
